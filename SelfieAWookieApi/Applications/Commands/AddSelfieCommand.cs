using MediatR;
using SelfieAWookieApi.Applications.DTO;
using System.Windows.Input;

namespace SelfieAWookieApi.Applications.Commands
{
    public class AddSelfieCommand : IRequest<SelfieDTOComplete>
    {
        #region Parameter
        public required SelfieDTOComplete Selfie { set; get; }
        #endregion
    }
}
