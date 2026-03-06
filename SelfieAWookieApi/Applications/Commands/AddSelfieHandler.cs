using MediatR;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookieApi.Applications.DTO;
using SelfieAWookies.Selfies.Domain;

namespace SelfieAWookieApi.Applications.Commands
{
    public class AddSelfieHandler : CommandSelfieRepository, IRequestHandler<AddSelfieCommand, SelfieDTOComplete>
    {
        #region contructeur
        public AddSelfieHandler(ISelfieRepository repository) : base(repository) { }
        #endregion

        #region method public IRequestHandler
        public Task<SelfieDTOComplete> Handle(AddSelfieCommand request, CancellationToken cancellationToken)
        {
            var result = _repository.Add(
                new Selfie()
                {
                    ImagePath   = request.Selfie.ImagePath,
                    WookieId    = request.Selfie.WookieId,
                    Title       = request.Selfie.Title,
                    Wookie      = request.Selfie.Wookie
                });

            _repository.UnitOfWork.SaveChanges();
            if (result is not null) request.Selfie.Id = result.Id;

            return Task.FromResult(request.Selfie);

        }
        #endregion
    }
}
