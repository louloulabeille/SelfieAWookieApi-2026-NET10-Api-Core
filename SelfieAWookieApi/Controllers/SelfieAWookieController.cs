using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookies.Selfies.Domain;
using System.Data;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfieAWookieController : ControllerBase
    //public class SelfieAWookieController(ILogger<SelfieAWookieController> logger): ControllerBase
    {
        #region constructeur
        public SelfieAWookieController(SelfieAWookieDbContext context)
        {
            _context = context;
        }

        #endregion


        #region private 
        //private readonly ILogger<SelfieAWookieController> _logger = logger ;
        private readonly SelfieAWookieDbContext _context;
        #endregion

        #region Public Methods

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //return this.StatusCode(StatusCodes.Status202Accepted);
            //return this.BadRequest("This is a bad request example");

            return this.Ok(_context.Selfies.ToArray()); 

            /*
            return this.Ok(Enumerable.Range(1, 10).Select(index => new Selfie
            {
                Id = index,
                Title = $"Selfie {index}",
                Wookie = new Wookie { Id = index, Name = $"Wookie {index}" }
            }).ToArray()); 
            */
        }
        #endregion

    }
}
