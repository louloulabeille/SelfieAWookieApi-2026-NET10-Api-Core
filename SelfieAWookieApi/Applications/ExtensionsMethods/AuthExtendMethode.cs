using Microsoft.Extensions.Primitives;

namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class AuthExtendMethode
    {
        extension (IServiceCollection service)
        {
            public IServiceCollection AddAuthentificationService()
            {
                return service;
            }
        }
    }
}
