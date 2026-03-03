using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookieApi.Applications.DTO;
using SelfieAWookieApi.Applications.ExtensionsMethods;
using SelfieAWookieApi.Applications.Security;
using System.Runtime.CompilerServices;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController( IConfiguration configuration, SignInManager<IdentityUser> signInManager) : ControllerBase
    {
        #region private fields
        //private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        #endregion

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> SaveAuth([FromBody] AuthDTO auth)
        {
            

            if (auth is null || string.IsNullOrEmpty(auth?.Login) || string.IsNullOrEmpty(auth?.Password))
            {
                return this.BadRequest("Problème avec l'enregistrement de votre compte." );
            }

            IdentityUser user = new()
            {
                UserName = auth.Name ?? auth.Login,
                Email = auth.Login,
            };

            // - mettre le mot de passe en paramètre pour qu'il le hash et le vérifie selon le paramétrage du mot de passe
            var succes = await _signInManager.UserManager.CreateAsync(user,auth.Password);

            if (succes.Succeeded) { 
                auth.Password = string.Empty;
                auth.Token = SecurityTokenGenerate.GenerateTokenJWT(_configuration, user);
                return this.Ok(auth);
            }

            return this.BadRequest(succes.Errors);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthDTO auth)
        {
            if ( string.IsNullOrEmpty(auth.Login) ||  string.IsNullOrEmpty(auth.Password) ) this.BadRequest("Authentication impossible.");

            /* 
             var user = await _userManager.FindByEmailAsync(auth.Login);

             if(user is not null && await _userManager.CheckPasswordAsync(user, auth.Password!))
             {
                 auth.Password = string.Empty;
                 auth.Token = SecurityTokenGenerate.GenerateTokenJWT(_configuration, user);
                 return this.Ok(auth);
             }*/

            // -- Recherche du compte user dans la base de données
            var user    = await _signInManager.UserManager.FindByEmailAsync(auth.Login);
            
            if (user is not null)
            {
                // -- comparaison entre les mots de passe et mise en activé du système de bloquer le compte si le nombre de 
                // -- tentative est supérieur de 3
                var result = await _signInManager.CheckPasswordSignInAsync(user, auth.Password!, true);

                if (result.Succeeded) { // -- ré&sultat positive
                    // password ok
                    auth.Password = string.Empty;
                    auth.Token = SecurityTokenGenerate.GenerateTokenJWT(_configuration, user);
                    return this.Ok(auth);
                }
                 if ( result.IsLockedOut ) // -- compte locked si tentative > 3
                {
                    return this.BadRequest("Problem, your account is locked out 10 minutes.");
                }
                   
            }
            return this.BadRequest("Invalid password or loggin. Good luck.");
        }
    }
}
