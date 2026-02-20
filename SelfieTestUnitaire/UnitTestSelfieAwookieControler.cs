using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SelfieAWookie.Core.Selfies.Infrastructure;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookieApi.Applications.DTO;
using SelfieAWookieApi.Controllers;
using SelfieAWookies.Selfies.Domain;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SelfieTestUnitaire
{
    public class UnitTestSelfieAwookieControler
    {
        #region private fields
        // context InMemorry pour les tests unitaires
        private readonly SelfieContextMemory _context;
        #endregion

        #region constructeur
        public UnitTestSelfieAwookieControler()
        {
            // initialisation du contexte de base de données en mémoire et ajout de données de test
            _context = new SelfieContextMemory();
            _context.Selfies.Add(new Selfie
            {
                Id = 1,
                Title = "Selfie 1",
                ImagePath = "path/to/image1.jpg",
                WookieId = 1,
                Wookie = new Wookie
                {
                    Id = 1,
                    Name = "Wookie 1",
                }
            });
            _context.Selfies.Add(new Selfie
            {
                Id = 2,
                Title = "Selfie 2",
                ImagePath = "path/to/image2.jpg",
                WookieId = 2,
                Wookie = new Wookie
                {
                    Id = 2,
                    Name = "Wookie 2"
                }
            });
            _context.SaveChanges();
        }
        #endregion

        [Fact]
        public void ShouldReturnGetAllSelfie()
        {
            //Arrange

            /* utilisation du mock - utiliser InMemory */
            /*
            var mockRepository = new Mock<ISelfieRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Selfie>
            {
                new Selfie
                {
                    Id = 1,
                    Title = "Selfie 1",
                    ImagePath = "path/to/image1.jpg",
                    WookieId = 1,
                    Wookie = new Wookie
                    {
                        Id = 1,
                        Name = "Wookie 1"
                    }
                },
                new Selfie
                {
                    Id = 2,
                    Title = "Selfie 2",
                    ImagePath = "path/to/image2.jpg",
                    WookieId = 2,
                    Wookie = new Wookie
                    {
                        Id = 2,
                        Name = "Wookie 2"
                    }
                }
            });
            */

            ISelfieRepository repository = new SelfieRepository(_context);

            //Données à retourner
            var controller = new SelfieAWookieController(repository);

            //Act
            var result = controller.GetAll();
            var okResult = result as OkObjectResult; // Cast du résultat en OkObjectResult
            IEnumerable<SelfieDTO>? selfiesDTO = okResult!.Value as IEnumerable<SelfieDTO>; // Cast de la valeur du résultat en IEnumerable<Selfie>

            //Assert
            Assert.NotNull(selfiesDTO); // Vérifie que le résultat n'est pas null
            Assert.True(selfiesDTO.Any()); // Vérifie que la collection contient au moins un élément
            Assert.IsType<SelfieDTO> (selfiesDTO.First()); // Vérifie que le type des éléments de la collection est Selfie
            Assert.Equal(1, selfiesDTO.First().NbSelfieFromWookie); // Vérifie que la collection contient exactement 2 éléments
        }
    }
}
