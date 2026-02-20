using SelfieAWookie.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Interface.Repository
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
