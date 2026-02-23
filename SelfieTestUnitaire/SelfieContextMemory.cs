using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieTestUnitaire
{
    // création d'une classe de contexte de base de données en mémoire pour les tests unitaires
    // cette classe hérite de la classe de contexte de base de données réelle
    // et utilise la méthode OnConfiguring pour configurer une base de données en mémoire
    public class SelfieContextMemory : SelfieAWookieDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // appel de la méthode de configuration de la base de données en mémoire
            optionsBuilder.UseInMemoryDatabase(databaseName: "Selfie-Dev");
        }
    }
}
