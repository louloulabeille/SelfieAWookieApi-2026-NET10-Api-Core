using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Core.Selfies.Infrastructure.Database;
using System.Text;

namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class AuthExtendMethode
    {
        private static readonly PasswordOptions _passwordOptions = new () {
            RequiredLength = 12, // - taille minimun du passworld
            RequireUppercase = true,
            RequiredUniqueChars = 1,
            RequireLowercase = true,
            RequireDigit = true,
            RequireNonAlphanumeric = true,
        };

        public static PasswordOptions Options() {  return _passwordOptions; }


        extension (IServiceCollection services)
        {
            /// <summary>
            /// Mise en place du framework Authentication.JwtBearer
            /// Mise en place de la 
            /// </summary>
            /// <returns></returns>
            public IServiceCollection AddAuthentificationService(IConfiguration configuration)
            {
                // clé de chiffrement du JwtBearer
                string key = configuration["Key:Symetrique"]??string.Empty;

                if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("Key JWT is null.");

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme   = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme               = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme      = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options => 
                { 
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateAudience = true,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,    // - doit donner un temps de vie pour le tokken
                    };
                });

                return services;
            }

            /// <summary>
            /// Paramétrage du User Identity
            /// </summary>
            /// <returns></returns>
            public IServiceCollection AddCustomOptionsIdentity()
            {

                services.AddDefaultIdentity<IdentityUser>(options => 
                {
                    //options.Password = new PasswordOptions()
                    //{
                    //    RequiredLength = 12, // - taille minimun du passworld
                    //    RequireUppercase = true,
                    //    RequiredUniqueChars = 1,
                    //    RequireLowercase = true,
                    //    RequireDigit = true,
                    //    RequireNonAlphanumeric = true,
                    //};
                    options.Password = _passwordOptions; // paramétrage du mot de passe lors de sa création
                    // - paramétrage de nb access failed et temps de blocage
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    // - paramétrage du user 
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;
                    
                    //options.SignIn.RequireConfirmedAccount  = true;
                    //options.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<SelfieAWookieDbContext>();

                return services;
            }
        }
    }
}
