using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Database.TypeConfiguration
{
    internal class WookieEntityTypeConfiguration : IEntityTypeConfiguration<Wookie>
    {
        public void Configure(EntityTypeBuilder<Wookie> builder)
        {
            builder.ToTable(nameof(Wookie));
            builder.HasKey(w => w.Id);

            // va incrémenter automatiquement l'Id à chaque nouvelle entrée
            builder.Property(w => w.Id).ValueGeneratedOnAdd();  

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
