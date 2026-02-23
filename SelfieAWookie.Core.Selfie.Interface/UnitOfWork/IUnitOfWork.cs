using SelfieAWookie.Core.Selfies.Interface.Repository;
using SelfieAWookie.Core.Selfies.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<T> Repository<T>() where T : class;

        public int SaveChanges();
    }
}
