using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookieApi.Applications.DTO;
using SelfieAWookieApi.Applications.Security;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController(UserManager<IdentityUser> userManager, IConfiguration configuration) : ControllerBase
    {
        #region private fields
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        #endregion

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> SaveAuth([FromBody] AuthDTO auth)
        {
            if(auth is null || (string.IsNullOrEmpty(auth?.Login) && string.IsNullOrEmpty(auth?.Password))) return this.BadRequest("Problème avec l'enregistrement de votre compte");

            IdentityUser user = new ()
            { 
                UserName = auth.Name ?? auth.Login,
                Email = auth.Login
            };

            var succes = await _userManager.CreateAsync(user);

            if (succes.Succeeded) { 
                auth.Login = string.Empty;
                auth.Token = SecurityTokenGenerate.GenerateTokenJWT(_configuration, user);
                return this.Ok(auth);
            }

            return this.BadRequest(succes.Errors);
        }
    }
}
