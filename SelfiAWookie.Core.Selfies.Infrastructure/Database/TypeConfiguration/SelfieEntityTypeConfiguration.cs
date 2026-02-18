using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfiAWookie.Core.Selfies.Infrastructure.Database.TypeConfiguration
{
    internal class SelfieEntityTypeConfiguration : IEntityTypeConfiguration<Selfie>
    {
        #region public methods

        public void Configure(EntityTypeBuilder<Selfie> builder)
        {
            throw 
                new NotImplementedException();
        }

        #endregion
    }
}
