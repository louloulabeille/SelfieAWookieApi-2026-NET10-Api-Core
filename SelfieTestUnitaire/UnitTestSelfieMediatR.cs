using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookieApi.Applications.Commands;
using SelfieAWookieApi.Applications.DTO;
using SelfieAWookieApi.Applications.Queries;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieTestUnitaire
{
    /// <summary>
    /// Classe de test des Selfies MediatR command - query & handler pour les tests unitaires
    /// </summary>
    public class UnitTestSelfieMediatR
    {
        #region Private Fields
        private readonly SelfieContextMemory _context = new ();
        private readonly Mock<IMediator> _mediator;
        private readonly SelfieRepository _repository;
        #endregion

        #region Constructeur
        public UnitTestSelfieMediatR()
        {
            _mediator = new Mock<IMediator>();
            _repository = new SelfieRepository(_context);

            this.Init();    // initialisation de la base de donnees
            
        }
        #endregion

        #region méthode init
        private void Init()
        {
            // -- supprime toutes les donneés
            _context.Selfies.RemoveRange(_context.Selfies.ToList());
            _context.Wookies.RemoveRange(_context.Wookies.ToList());

            // initialisation du contexte de base de données en mémoire et ajout de données de test
            _context.Wookies.Add(new Wookie
            {
                Id = 1,
                Name = "Wookie 1"
            });

            _context.Wookies.Add(new Wookie
            {
                Id = 2,
                Name = "Wookie 2"
            });

            _context.Selfies.Add(new Selfie
            {
                Title = "Selfie 1",
                ImagePath = "path/to/image1.jpg",
                WookieId = 1,
            });

            _context.Selfies.Add(new Selfie
            {
                Title = "Selfie 2",
                ImagePath = "path/to/image2.jpg",
                WookieId = 2
            });
            _context.SaveChanges();
        }

        #endregion

        #region XUnit Test

        [Fact]
        public async Task ShouldAddSelfieMediatR()
        {
            //arrange
            SelfieDTOComplete selfie = new SelfieDTOComplete() {
                Id = 0,
                Title = "Selfie 3",
                ImagePath = "path/to/image3.jpg",
                WookieId = 1,
            };
            this.Init ();
            _mediator.Setup(m => m.Send(It.IsAny<AddSelfieCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(selfie).Verifiable("Notification was not sent."); ;


            // Act
            
            var handler = new AddSelfieHandler(_repository);
            var result = await handler.Handle(new AddSelfieCommand() { Selfie = selfie }, default);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);

        }

        [Fact]

        public async Task ShouldGetAllMediatR() { 
            //arrange
            this.Init();
            IEnumerable<SelfieDTO> selfies = Enumerable.Empty<SelfieDTO>();
            _mediator.Setup(m => m.Send(It.IsAny<SelectAllSelfiesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(selfies).Verifiable("Notification was not sent.");

            //Act
            var handler = new SelectAllSelfieHandler(_repository);
            var result = await handler.Handle(new SelectAllSelfiesQuery()
            {
                WookieId = null
            }, default);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == 2);

        }

        [Fact]
        public async Task ShouldGetAllForOneWookieMediatR()
        {
            //arrange
            this.Init();
            IEnumerable<SelfieDTO> selfies = Enumerable.Empty<SelfieDTO>();
            _mediator.Setup(m => m.Send(It.IsAny<SelectAllSelfiesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(selfies).Verifiable("Notification was not sent.");
            Selfie selfie = new Selfie() { Title = "Selfie n°3", ImagePath = "path/image3", WookieId=1 };
            _repository.Add(selfie);
            _repository.UnitOfWork.SaveChanges();


            //Act
            var handler = new SelectAllSelfieHandler(_repository);
            var result = await handler.Handle(new SelectAllSelfiesQuery()
            {
                WookieId = 1
            }, default);

            //Assert
            Assert.NotNull(result);
            Assert.True(2 == result.Count());
            Assert.Equal(result.First().NbSelfieFromWookie, 2);

        }

        #endregion

    }
}
