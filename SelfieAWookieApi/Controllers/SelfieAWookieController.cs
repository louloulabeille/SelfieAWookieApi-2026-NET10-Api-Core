using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookieApi.Applications.DTO;
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
        /*
        public SelfieAWookieController(ISelfieRepository repository)
        {
            _repository = repository;
        }
        */
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
            var query = _repository.GetAll().Select(item => new SelfieDTO()
            {
                Title = item.Title,
                WookieId = item.WookieId,
                NbSelfieFromWookie = item.Wookie?.Selfies?.Count
            });


            return query is IEnumerable<SelfieDTO> and not null ? Ok(query)
                : this.BadRequest("Erreur de remonter des données ");
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

        [HttpPost("Add-Selfie")]
        public IActionResult Add(SelfieDTOComplete selfie)
        {
            IActionResult result = this.BadRequest("Error request add Selfie.");

            if (selfie == null) return this.BadRequest("Le selfie est vide.");

            var model = _repository.Add(new Selfie() {
                Title = selfie.Title!,
                ImagePath = selfie.ImagePath,
                WookieId = selfie.WookieId,
                Wookie = selfie.Wookie
            });
            
            _repository.UnitOfWork.SaveChanges();
            if (model != null) {
                selfie.Id = model.Id;
                result = this.Ok(model);
            }
            
            return result;
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            IActionResult result = this.BadRequest("Error request get Selfie by id.");

            if (id <= 0) return this.BadRequest("Id > 0 && required"); 

            var model = _repository.GetAll(id).Select(item=> new SelfieDTOComplete() {
                Id = item.Id,
                Title = item.Title,
                ImagePath = item.ImagePath,
                WookieId = item.WookieId,
                Wookie = item.Wookie
            });

            if (model != null) result = this.Ok(model);

            return result;
        }

        #endregion

    }
}
