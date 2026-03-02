using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookieApi.Applications.DTO;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager) : ControllerBase
    {
        #region private fields
        private readonly UserManager<IdentityUser> _userManager = userManager;
        #endregion

        [Route("SaveAuth")]
        [HttpPost]
        public async Task<IActionResult> SaveAuth([FromBody] AuthDTO auth)
        {
            if(auth is not null || (string.IsNullOrEmpty(auth?.Login) && string.IsNullOrEmpty(auth?.Password))) return this.BadRequest("Problème avec l'enregistrement de votre compte");

            var user = new IdentityUser()
            { 
                UserName = auth.Name,
                Email = auth.Login
            };

            var succes = await _userManager.CreateAsync(user);

            if (succes.Succeeded) { 
                auth.Login = string.Empty
                return this.Ok(auth);
            }

            return this.BadRequest(succes.Errors);
        }
    }
}
