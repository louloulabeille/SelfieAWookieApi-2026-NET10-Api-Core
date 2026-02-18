using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfiAWookie.Core.Selfies.Infrastructure.Database.TypeConfiguration
{
    internal class WookieEntityTypeConfiguration : IEntityTypeConfiguration<Wookie>
    {
        public void Configure(EntityTypeBuilder<Wookie> builder)
        {
            builder.ToTable("Wookie");
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
