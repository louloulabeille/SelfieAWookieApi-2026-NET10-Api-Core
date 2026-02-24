using System;
using System.Collections.Generic;
using System.Text;
using SelfieAWookie.Core.Framework;
using SelfieAWookies.Selfies.Domain;

namespace SelfieAWookie.Core.Selfies.Interface.Repository
{
    public interface ISelfieRepository : IDisposable, IRepository
    {
        public IEnumerable<Selfie> GetAll();
        public Selfie Add(Selfie selfie);
        public IEnumerable<Selfie> GetAll(int id);

    }
}
