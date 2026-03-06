using SelfieAWookie.Core.Selfies.Interface.Repository;

namespace SelfieAWookieApi.Applications.Commands
{
    public class CommandSelfieRepository(ISelfieRepository repository)
    {
        #region private Fields
        internal readonly ISelfieRepository _repository = repository;
        #endregion
    }
}
