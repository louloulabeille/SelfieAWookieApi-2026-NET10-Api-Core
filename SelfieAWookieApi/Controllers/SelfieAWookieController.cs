using Microsoft.AspNetCore.Components.Forms;
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
using System.Reflection.Metadata;

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
        public SelfieAWookieController(SelfieAWookieDbContext context, IHostEnvironment host)
        {
            _unitOfWork = new UnitOfWork(context);
            _host = host;
        }
        #endregion


        #region private fields
        //private readonly ILogger<SelfieAWookieController> _logger = logger ;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _host;
        #endregion

        #region Public Methods

        /*[HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var model = _unitOfWork.Repository<Selfie>().GetAll()
                .Select(x=> new SelfieDTO{ 
                Title = x.Title,
                WookieId = x.WookieId,
                NbSelfieFromWookie = _unitOfWork.Repository<Selfie>().Where(i=>i.WookieId == x.WookieId).Count()
                });
            
            if (model is null) return this.BadRequest("Problem request.");;

            return this.Ok(model);

        }*/
        [Route("AddSelfie")]
        //[HttpPost("Add-Selfie")]
        [HttpPost]
        public IActionResult AddSelfie(SelfieDTOComplete selfie)
        {
            if (selfie is null) return BadRequest("Le selfie ne peut pas être null.");

            var retour = _unitOfWork.Repository<Selfie>().Add(new Selfie()
            {
                Id = selfie.Id,
                Title = selfie.Title!,
                ImagePath = selfie.ImagePath,
                WookieId = selfie.WookieId,
                // gestion du wookie lors de l'ajout si c'est son premier message
                Wookie = selfie.Wookie is not null ? new Wookie() {
                    Id = selfie.Wookie.Id,
                    Name = selfie.Wookie.Name
                } : null
            });

            selfie.Id = retour.Id;

            //on ne le met en place pour le moment sinon la base va être pourrie
            _unitOfWork.SaveChanges();
            return Ok(selfie);
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? id)
        {
            var model = _unitOfWork.Repository<Selfie>().GetAll().AsQueryable();

            if (id is null || id == 0)
            {

                var retour = model.Select(x => new SelfieDTO
                {
                    Title = x.Title,
                    WookieId = x.WookieId,
                    NbSelfieFromWookie = _unitOfWork.Repository<Selfie>()
                    .Where(i => i.WookieId == x.WookieId).Count()
                });

                if (retour is null) return this.BadRequest("Problem request."); ;

                return this.Ok(retour);
            }

            if (id < 0) return this.BadRequest("id > 0 && required");

            var wookie = _unitOfWork.Repository<Wookie>().Where(x => x.Id == id).FirstOrDefault();
            // eviter la récurcivité infinie du wookie dans le selfie et du selfie dans le wookie
            WookieDTONoSelfie wookieDTO = new()
            {
                Id = wookie!.Id,
                Name = wookie.Name,
            };

            var item = model.Where(x => x.WookieId == id).Select(item => new SelfieDTOComplete() {
                Id = item.Id,
                Title = item.Title,
                ImagePath = item.ImagePath,
                WookieId = item.WookieId,
                Wookie = wookieDTO
            }).ToList();

            if (item is null) return this.BadRequest("Problem request.");

            return this.Ok(item);
        }

        [Route("Photos")]
        [HttpPost]
        //Mise en place du téléchargement de l'image
        public async Task<IActionResult> GetPicture([FromQuery]IFormFile img)
        {
            try
            {
                string filePath = Path.Combine(_host.ContentRootPath, @"images\selfies");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, img.FileName);

                using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
                await img.CopyToAsync(stream);

                return this.Ok(img.FileName);
            }
            catch (Exception e)
            {
                return this.BadRequest("Error " + e.Message);
            }
            
        }
        #endregion

    }
}
