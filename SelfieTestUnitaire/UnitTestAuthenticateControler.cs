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
                {"Key:Symetrique","ab7a0a70aec592e084f3dab33ba3ca4052a55b0d8f3c03022e58a8096a4df5265f1e1d992119eacb5135dd26937176b7ad7df3d542d365a538411be77f2ed8c8"}
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
