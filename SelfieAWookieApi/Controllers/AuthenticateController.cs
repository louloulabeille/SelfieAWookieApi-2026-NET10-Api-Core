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
    public class AuthenticateController(SelfieAWookieDbContext context,UserManager<IdentityUser> userManager
        , IConfiguration config) 
        : Controller
    {
        #region private field
        private readonly SelfieAWookieDbContext     _context = context;
        private readonly UserManager<IdentityUser>  _userManager = userManager;
        private readonly  IConfiguration            _config = config;
        #endregion


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            if (loginDTO is null ) return this.BadRequest("Login ou mot de passe manquand.");

            var user = await _userManager.FindByEmailAsync(loginDTO.Login);

            if (user != null && loginDTO.Password is not null && 
                await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return this.Ok(new LoginDTO()
                {
                    Login = user.Email!,
                    Name = user.UserName,
                    Token = SecurityTokenGenerate.GenerateJwtToken(user, _config)

                });
            }

            return this.BadRequest("Problem dans la connexion.");
        }
    }
}
