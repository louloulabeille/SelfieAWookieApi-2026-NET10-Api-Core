using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Infrastructure.Database;
using SelfieAWookieApi.Applications.DTO;
using SelfieAWookieApi.Applications.Security;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    public class AuthenticateController(UserManager<IdentityUser> userManager
        , IConfiguration config) 
        : Controller
    {
        #region private field
        //private readonly SelfieAWookieDbContext     _context = context;
        private readonly UserManager<IdentityUser>  _userManager = userManager;
        private readonly IConfiguration            _config = config;
        #endregion

        /// <summary>
        /// Vérification du login avec la création du token
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            if (loginDTO is null || loginDTO.Password is null) return this.BadRequest("Login ou mot de passe manquand.");

            var user = await _userManager.FindByEmailAsync(loginDTO.Login);

            if (user != null && loginDTO.Password is not null && 
                await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return this.Ok(new LoginDTO()
                {
                    Login       = user.Email!,
                    Name        = user.UserName,
                    Password    = "",
                    Token       = SecurityTokenGenerate.GenerateJwtToken(user, _config)

                });
            }

            return this.BadRequest("Problem dans la connexion.");
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] LoginDTO loginDTO) {

            if (loginDTO is null ||  loginDTO.Password is null) return this.BadRequest("Login ou mot de passe manquand.");

            var user = new IdentityUser(loginDTO.Login) { 
                Email = loginDTO.Login,
                UserName = loginDTO.Name ?? loginDTO.Login,
            };

            var success = await _userManager.CreateAsync(user, loginDTO.Password);

            if (success.Succeeded)
            {
                loginDTO.Token = SecurityTokenGenerate.GenerateJwtToken(user, _config);
                loginDTO.Password = "";
            }
            else
                return BadRequest(success.Errors);
            

            return this.Ok(loginDTO);
        
        }
    }
}
