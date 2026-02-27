using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Infrastructure.Database;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookieApi.Applications.ExtensionsMethods;

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
builder.Services.AddCrossOrigin();

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.Authority = "https://{--your-authority--}";
    jwtOptions.Audience = "https://{--your-audience--}";
});

#endregion

/*
 * 
 lancement de l'application
 *
 */

var app = builder.Build();

#region Middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    #region lancement du swagger
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

    #endregion 

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(CrosSelfieExtend.DefaultPolicyName);

app.MapControllers();

#endregion

app.Run();
