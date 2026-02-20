using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Framework
{
    // Interface pour le pattern Unit of Work,
    // qui permet de regrouper plusieurs opérations de base de données en une seule transaction
    public interface IUnitOfWork
    {
        public int SaveChanges();
    }
}
