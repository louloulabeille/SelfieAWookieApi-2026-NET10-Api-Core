using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Framework
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
