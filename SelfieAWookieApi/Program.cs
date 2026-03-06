using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Core.Selfies.Infrastructure.Database;
using SelfieAWookie.Core.Selfies.Infrastructure.Loggers;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookieApi.Applications.ExtensionsMethods;
using SelfieAWookieApi.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

#region Swagger
//ajout de swagger pour la documentation de l'API & il faut installer le package
//Swashbuckle.AspNetCore pour que ça fonctionne
builder.Services.AddSwaggerGen();
#endregion

#region CrossOrigin
//ajout de la politique de cross origin pour autoriser les requetes depuis n'importe quelle origine
// au niveau de l'application autant le mettre en place directement au niveau du serveur
builder.Services.AddCorsOrigin();

#endregion

#region dbContext
//mise en place de la connection vers la base de données
//recherche des paramètres de connection vers la base 
string? stringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

//mise en place de la connection vers la base postgreSQL
// injection de dépendance du dbContext en scoped 
builder.Services.AddDbContext<SelfieAWookieDbContext>(options =>
    options.UseNpgsql(stringConnection));
#endregion

#region injection de dépendance
//builder.Services.AddScoped<ISelfieRepository,SelfieRepository>();
builder.Services.AddInjectionRepository();
#endregion

#region JWT
// TODO : mise en place de l'authentification JWT

// mise en place de Identity.Ui
builder.Services.AddCustonIdentityUser();

builder.Services.AddCustomlsAuthentification(builder.Configuration);

builder.Services.AddServiceSecurityOptionsExtend(builder.Configuration);
#endregion

#region MediatR
builder.Services.AddInjectionMediatR();
#endregion

//builder.Configuration["HTTPS_PORT"] = "7071";
/*
 * 
 lancement de l'application
 *
 */

#region Ajout Provider Logger
builder.Logging.AddProvider(new SelfieLoggerProvider());
#endregion

var app = builder.Build();

#region Middleware

app.UseMiddleware<LogRequestMiddleware>();

// Configure the HTTP request pipeline.1
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage(); 

}

if (app.Environment.IsStaging() || app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    #region lancement du swagger en production aussi
    // lancement du swagger
    app.UseSwagger();
    app.UseSwaggerUI(); // lien https://localhost:7030/swagger/index.html

    // lien vers le swagger pour le mettre à la racine
    /*app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    */
    ;
    #endregion
}

// le mieux c'est de le gérer au niveau des serveur Web
app.UseCors(CorsSelfieExtend.DefaultPolicyName); // utilisation de la politique de cross origin pour autoriser les requetes depuis n'importe quelle origine


app.UseHttpsRedirection();

app.UseAuthentication();        //-- mise en place de l'authentification n'importe laquelle
app.UseAuthorization();

app.MapControllers();

#endregion
app.Logger.LogInformation($"{DateTime.Now} : start application.");

app.Run();

