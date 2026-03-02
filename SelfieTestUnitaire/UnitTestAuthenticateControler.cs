using Castle.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;
using SelfieAWookieApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieTestUnitaire
{
    public class UnitTestAuthenticateControler
    {
        #region private fields
        // context InMemorry pour les tests unitaires
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly SelfieContextMemory _context;


        #endregion

        #region
        public UnitTestAuthenticateControler()
        {
            _context = new SelfieContextMemory();
            _config = InitConfig();
        }
        #endregion

        #region private methode Init
        /// <summary>
        /// méthode d'initialisation de la configuration
        /// </summary>
        /// <returns></returns>
        private Microsoft.Extensions.Configuration.IConfiguration InitConfig()
        {

            // intialisation de IConfiguration
            var inMemorySettings = new Dictionary<string, string>()
            {
                {"Key:Symetrique","ploufplouf"}
            };

            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!).Build();
            return config;
        }
        #endregion


        #region methode Test 
        /// <summary>
        /// méthode de test pour ajouter un utilisateur dans la base et retourne un token
        /// </summary>
        [Fact]
        public void ShouldReturnTokenRegister ()
        {
            //Arrange

            
        }


        #endregion
    }
}
