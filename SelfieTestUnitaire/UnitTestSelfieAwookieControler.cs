using SelfieAWookieApi.Controllers;
using SelfieAWookies.Selfies.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SelfieTestUnitaire
{
    public class UnitTestSelfieAwookieControler
    {
        [Fact]
        public void ShouldReturnGetAllSelfie()
        {
            //Arrange


            //Données à retourner
            var controller = new SelfieAWookieController( );

            //Act
            var result = controller.GetAll();
            var okResult = result as OkObjectResult; // Cast du résultat en OkObjectResult
            IEnumerable<Selfie>? selfies = okResult!.Value as IEnumerable<Selfie>; // Cast de la valeur du résultat en IEnumerable<Selfie>

            //Assert
            Assert.NotNull(selfies); // Vérifie que le résultat n'est pas null
            Assert.True(selfies.Any()); // Vérifie que la collection contient au moins un élément
            Assert.IsType<Selfie> (selfies.First()); // Vérifie que le type des éléments de la collection est Selfie
        }
    }
}
