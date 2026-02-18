using Microsoft.EntityFrameworkCore;
using SelfiAWookie.Core.Selfies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
//ajout de swagger pour la documentation de l'API & il faut installer le package
//Swashbuckle.AspNetCore pour que ça fonctionne
builder.Services.AddSwaggerGen();   

//mise en place de la connection vers la base de données
//recherche des paramètres de connection vers la base 
string? stringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

//mise en place de la connection vers la base sqlserveur
#region dbContext
builder.Services.AddDbContext<SelfieAWookieDbContext>(options =>
    options.UseSqlServer(stringConnection));
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
