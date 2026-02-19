using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Database.TypeConfiguration
{
    internal class SelfieEntityTypeConfiguration : IEntityTypeConfiguration<Selfie>
    {
        #region public methods

        public void Configure(EntityTypeBuilder<Selfie> builder)
        {
            builder.ToTable(nameof(Selfie));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired();

            builder.HasOne(x => x.Wookie).WithMany(x => x.Selfies);

            builder.Property(x => x.ImagePath).IsRequired(false).HasMaxLength(250);
        }

        #endregion
    }
}
