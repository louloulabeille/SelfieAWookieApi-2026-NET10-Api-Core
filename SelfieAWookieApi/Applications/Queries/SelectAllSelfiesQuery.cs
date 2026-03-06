using MediatR;
using SelfieAWookieApi.Applications.DTO;

namespace SelfieAWookieApi.Applications.Queries
{
    public class SelectAllSelfiesQuery : IRequest<IEnumerable<SelfieDTO>>
    {
        #region Fields
        public int WookieId { get; set; }
        #endregion

    }
}
