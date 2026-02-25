using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfieAWookie.Core.Selfies.Infrastructure.Database.TypeConfiguration
{
    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable(nameof(Picture));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Url)
                .IsRequired();
        }
    }
}
