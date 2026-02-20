using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;

namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    // classe d'extension pour l'injection de dépendance du repository
    public static class InjectionRepository
    {
        #region extension method
        extension(IServiceCollection services)
        {
            public IServiceCollection AddInjectionRepository()
            {
                return services.AddScoped<ISelfieRepository, SelfieRepository>();
            }
        }
        #endregion

    }
}
