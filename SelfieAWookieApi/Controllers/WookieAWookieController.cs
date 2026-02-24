using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Infrastructure.UnitOfWork;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookies.Selfies.Domain;

namespace SelfieAWookieApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WookieAWookieController : ControllerBase
    {
        #region constructeur
        public WookieAWookieController(SelfieAWookieDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }
        #endregion

        #region private fields

        private readonly UnitOfWork _unitOfWork;

        #endregion


        #region méthode du controller

        [HttpGet]
        public IActionResult GetAll()
        {
            IRepository<Wookie> model = _unitOfWork.Repository<Wookie>();

            if (model is null) return this.BadRequest("Problem request."); ;
            
            return this.Ok(model.GetAll());
        }

        #endregion
    }
}
