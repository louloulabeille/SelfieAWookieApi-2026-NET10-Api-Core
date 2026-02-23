using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Infrastructure.UnitOfWork;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookie.Core.Selfies.Interface.UnitOfWork;
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
        public SelfieAWookieController(SelfieAWookieDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }
        #endregion


        #region private fields
        //private readonly ILogger<SelfieAWookieController> _logger = logger ;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Public Methods

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var item = _unitOfWork.Repository<Selfie>().GetAll()
                .Select(x=> new SelfieDTO{ 
                Title = x.Title,
                WookieId = x.WookieId,
                NbSelfieFromWookie = _unitOfWork.Repository<Selfie>().Where(i=>i.WookieId == x.WookieId).Count()
                });


            return this.Ok(item);

        }

        [HttpPost("Add-Selfie")]
        public IActionResult AddSelfie(SelfieDTOComplete selfie)
        {
            if (selfie is null) return BadRequest("Le selfie ne peut pas être null.");

            var entity = new Selfie
            {
                Id = selfie.Id,
                Title = selfie.Title,
                ImagePath = selfie.ImagePath,
                WookieId = selfie.WookieId,
                Wookie = selfie.Wookie
            };

            var retour = _unitOfWork.Repository<Selfie>().Add(entity);

            var retourDTO = new SelfieDTOComplete
            {
                Id = retour.Id,
                Title = retour.Title,
                ImagePath = retour.ImagePath,
                WookieId = retour.WookieId,
                Wookie = retour.Wookie
            };
            //_unitOfWork.SaveChanges();
            return Ok(retourDTO);
        }
        #endregion

    }
}
