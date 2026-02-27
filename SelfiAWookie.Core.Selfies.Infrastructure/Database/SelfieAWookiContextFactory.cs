using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Database
{
    internal class SelfieAWookiContextFactory : IDesignTimeDbContextFactory<SelfieAWookieDbContext>
    {
        public SelfieAWookieDbContext CreateDbContext(string[] args)
        {

            ConfigurationBuilder configurationBuilder = new();
            string pathJson = Path.Combine(Directory.GetCurrentDirectory(), "Settings", "appsettings.Development.json");

            configurationBuilder.AddJsonFile(pathJson, optional: false, reloadOnChange: true);

            IConfigurationRoot builder = configurationBuilder.Build();

            //Console.WriteLine("Connection String: " + builder.GetConnectionString("DefaultConnection"));

            var optionBuilder = new DbContextOptionsBuilder<SelfieAWookieDbContext>();
            //optionBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Selfie-Dev;Trusted_Connection=True;TrustServerCertificate=True;");
            //optionBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=Selfie-Dev;User Id=sa;Password=ieupn486;");
            optionBuilder.UseNpgsql(builder.GetConnectionString("DefaultConnection"));
            return new SelfieAWookieDbContext(optionBuilder.Options);

        }
    }
}
