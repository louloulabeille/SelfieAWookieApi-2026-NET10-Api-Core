using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Repository
{
    // Classe repository pour les selfies qui implémente l'interface ISelfieRepository
    // et l'interface IDisposable pour la gestion des ressources
    public class SelfieRepository : ISelfieRepository , IRepository
    {
        #region private Flields
        private readonly SelfieAWookieDbContext _context;
        private bool disposed = false;
        #endregion

        #region public Fields
        public IUnitOfWork UnitOfWork => _context;
        #endregion

        #region constructeur
        public SelfieRepository(SelfieAWookieDbContext context) => _context = context;
        #endregion

        #region method interface IDisposable & ISelfieRepository

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Selfie> GetAll()
        {
            return _context.Selfies.Include(item => item.Wookie).Select(item=> 
                new Selfie()
                {
                    Id = item.Id,
                    Title = item.Title,
                    ImagePath = item.ImagePath,
                    WookieId = item.WookieId,
                    Wookie = new Wookie()
                    {
                        Id      = item.Wookie.Id,
                        Name    = item.Wookie.Name,
                        Selfies = item.Wookie.Selfies,
                    }
            });
        }
        #endregion

        #region methods asynchrone
        public async Task<IEnumerable<Selfie>> GetAllAsync()
        {
            return await _context.Selfies.Include(item => item.Wookie).Select(item =>
                new Selfie()
                {
                    Id = item.Id,
                    Title = item.Title,
                    ImagePath = item.ImagePath,
                    WookieId = item.WookieId,
                    Wookie = new Wookie()
                    {
                        Id      = item.Wookie.Id,
                        Name    = item.Wookie.Name,
                        Selfies = item.Wookie.Selfies,
                    }
                }).ToListAsync();
        }

        #endregion

    }
}
