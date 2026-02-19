using System;
using System.Collections.Generic;
using System.Text;
using SelfieAWookies.Selfies.Domain;

namespace SelfieAWookie.Core.Selfies.Interface.Repository
{
    public interface ISelfieRepository : IDisposable
    {
        public IEnumerable<Selfie> GetAll();


    }
}
