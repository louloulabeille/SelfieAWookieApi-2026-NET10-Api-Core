using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Repository
{
    //Repository générique qui implémente l'interface IRepository<T>
    //et l'interface IDisposable pour la gestion des ressources
    public class  Repository<T> : IRepository<T> where T : class
    {
        #region private Flields
        protected readonly SelfieAWookieDbContext _context;
        #endregion

        #region constructor
        public Repository(SelfieAWookieDbContext context)
        {
            _context = context;
        }
        #endregion


        #region methode implementer par l'interface IRepostory<T> 
        //ajout de virtual pour permettre la surcharge dans les classes dérivées
        //Les méthodes de base pour les opérations CRUD,
        //qui peuvent être surchargées dans les classes dérivées
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {   
           return _context.Set<T>().ToList<T>();
        }

        public virtual T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
        #endregion

       
    }
}
