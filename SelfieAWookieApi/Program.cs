using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;

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
builder.Services.AddScoped<ISelfieRepository,SelfieDepository>();

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

app.MapControllers();

#endregion

app.Run();
