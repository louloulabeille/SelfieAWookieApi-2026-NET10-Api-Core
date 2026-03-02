using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookieApi.Applications.DTO;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IActionResult SaveAuth([FromBody] AuthDTO auth)
        {
            if(auth is not null || (string.IsNullOrEmpty(auth?.Login) && string.IsNullOrEmpty(auth?.Password))) return this.BadRequest("Problème avec l'enregistrement de votre compte");

            

            return this.Ok();
        }
    }
}
