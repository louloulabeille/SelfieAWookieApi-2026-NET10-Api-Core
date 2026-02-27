using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Core.Selfies.Infrastructure.Database;
using System.Text;

namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class AuthentificationExtend
    {
        extension(IServiceCollection services)
        {
            /// <summary>
            /// installer le framework Authentification Jbearer mais avant au niveau de la base il faut installer
            /// entity identity framwork et créer les tables en migrant
            /// </summary>
            /// <param name="config"></param>
            /// <returns></returns>
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
                        ValidateLifetime = true,    // durée de vie à paramétrer lors de la création du token envoyer vers l'user
                    };
                });

                return services;
            }


            /// <summary>
            /// Mise en place du paramétrage par défaut de IdentityUser
            /// par exemple la taille du mot de passe s'il faut un mail de confirmation
            /// etc 
            /// installer le framework Identity.Ui
            /// </summary>
            /// <returns></returns>
            public IServiceCollection AddCustonIdentityUser()
            {
                services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password = new PasswordOptions()
                    {
                        RequiredLength = 12,
                        RequireUppercase = true,
                        RequiredUniqueChars = 1,
                        RequireLowercase = true,
                        RequireDigit = true,
                        RequireNonAlphanumeric = true,
                    };
                    //options.SignIn.RequireConfirmedEmail = true;
                    //options.SignIn.RequireConfirmedAccount = true;
                })
                        .AddEntityFrameworkStores<SelfieAWookieDbContext>();

                return services;
            }
        }
    }
}
