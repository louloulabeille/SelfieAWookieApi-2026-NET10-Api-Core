using MediatR;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookieApi.Applications.DTO;

namespace SelfieAWookieApi.Applications.Queries
{
    public class SelectAllSelfieHandler(ISelfieRepository repository) : IRequestHandler<SelectAllSelfiesQuery, IEnumerable<SelfieDTO>>
    {
        #region private Fields
        private readonly ISelfieRepository _repository = repository;
        #endregion

        #region method implement IRequestHandler

        public async Task<IEnumerable<SelfieDTO>> Handle(SelectAllSelfiesQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.GetAll(request.WookieId).Select(item=> new SelfieDTO()
            {
                Title = item.Title,
                WookieId = item.WookieId,
                NbSelfieFromWookie = item.Wookie?.Selfies?.Count,
            }).ToList();

            //return Task.FromResult(result);

            return result;
            
        }

        #endregion
    }
}
