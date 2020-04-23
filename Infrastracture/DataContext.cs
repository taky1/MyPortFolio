using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Infrastracture
{
 public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)       
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<ProtfolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id = Guid.NewGuid(),
                    Avatar = "avatar.jpg",
                    FullName = "khaled saadeni",
                    Profil = "Microsoft MVP / .NET Consultant"
                });
        }

        public DbSet<Owner> Owner { get; set; }

        public DbSet<ProtfolioItem> ProfolioItems { get; set; }


    }
}
