using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class AuthentificationExtend
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddCustomlsAuthentification(IConfiguration config)
            {
                // récupération de la key de chiffrement qui est dans le le settings
                string key = config["Key:Symetrique"]?? string.Empty;

                services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options => {

                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                    };
                });

                return services;
            }
        }
    }
}
