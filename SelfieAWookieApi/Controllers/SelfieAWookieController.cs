using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookie.Core.Selfies.Interface.Repository;
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
        public SelfieAWookieController(ISelfieRepository repository)
        {
            _repository = repository;
        }

        #endregion


        #region private fields
        //private readonly ILogger<SelfieAWookieController> _logger = logger ;
        //private readonly SelfieAWookieDbContext? _context;
        private readonly ISelfieRepository _repository;
        #endregion

        #region Public Methods

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //return this.StatusCode(StatusCodes.Status202Accepted);
            //return this.BadRequest("This is a bad request example");

            //return this.Ok(_context.Selfies.ToList()); 

            if (_repository == null) return this.NotFound("Aucun enregistrement trouvé");
            /*
            var query = _context.Selfies.Include(x => x.Wookie).Select(
                (x)=>
                    new Selfie()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ImagePath = x.ImagePath,
                        WookieId = x.WookieId,
                        Wookie = x.Wookie
                    }
                ).ToList();
            */
            return _repository.GetAll() is IEnumerable<Selfie> selfies ? Ok(selfies) : this.BadRequest("Erreur de remonter des données ");
            /*
            var query = from selfie in _context.Selfies
                        join wookie in _context.Wookies on selfie.WookieId equals wookie.Id
                        select new Selfie
                        {
                            Id = selfie.Id,
                            Title = selfie.Title,
                            ImagePath = selfie.ImagePath,
                            WookieId = selfie.WookieId,
                            Wookie = wookie
                        };
            */


        }
        #endregion

    }
}
