using SelfieAWookie.Core.Selfies.Infrastructure.Configuration;

namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class SecurityOptionsExtends
    {
        extension(IServiceCollection services)
        {
            public void AddServiceSecurityOptionsExtend (IConfiguration configuration)
            { 
                services.Configure<SecurityOptions>(configuration.GetSection("Key"));
            }
        }
    }
}
