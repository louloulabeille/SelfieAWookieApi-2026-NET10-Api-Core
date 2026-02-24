using SelfieAWookie.Core.Selfies.Infrastructure.Repository;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookie.Core.Selfies.Interface.UnitOfWork;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region private fields
        private readonly SelfieAWookieDbContext _context;
        private bool _disposed = false;
        private readonly Dictionary<Type, object> _repositories = new();
        #endregion

        #region constructors
        public UnitOfWork(SelfieAWookieDbContext context)
        {
            _context = context;
        }
        #endregion


        public IRepository<T> Repository<T>() where T : class
        {
            var typeName = typeof(T);
 
            if (_repositories.ContainsKey(typeName))
            {
                return (Repository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);

            return repository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


        #region implemention de l'interface IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
