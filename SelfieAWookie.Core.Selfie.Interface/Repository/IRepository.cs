using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Interface.Repository
{
    // Interface générique pour les repositories, qui peut être utilisée pour différents types d'entités
    public interface IRepository<T> where T : class
    {
        public  IEnumerable<T> GetAll();
        public  T? GetById(int id);
        public  void Add(T entity);
        public  void Update(T entity);
        public  void Delete(T entity);
        public IEnumerable<T> Where(Func<T, bool> predicate);
    }
}
