using Microsoft.EntityFrameworkCore;
using SelfiAWookie.Core.Selfies.Infrastructure.Database.TypeConfiguration;
using SelfieAWookies.Selfies.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfiAWookie.Core.Selfies.Infrastructure
{
    public class SelfieAWookieDbContext : DbContext
    {
        public SelfieAWookieDbContext(DbContextOptions<SelfieAWookieDbContext> options) : base(options)
        {
        }

        #region dbSet
        public DbSet<Selfie> Selfies { get; set; }
        public DbSet<Wookie> Wookies { get; set; }
        #endregion

        #region protected overrides
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations entre les tables 
            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WookieEntityTypeConfiguration());  

            //la configuration on peut la faire directement ici
            /*modelBuilder.Entity<Selfie>()
                .HasOne(s => s.Wookie)
                .WithMany()
                .HasForeignKey("WookieId")
                .OnDelete(DeleteBehavior.Cascade);*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        #endregion


    }
}
