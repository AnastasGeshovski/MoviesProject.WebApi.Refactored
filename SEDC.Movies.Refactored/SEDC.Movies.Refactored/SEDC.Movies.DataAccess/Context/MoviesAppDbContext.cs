using Microsoft.EntityFrameworkCore;
using SEDC.Movies.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.DataAccess.Context
{
    //Microsoft.EntityFrameworkCore.SqlServer 3.1.19
    //Microsoft.EntityFrameworkCore.SqlServer.Design 1.1.6
    //Microsoft.EntityFrameworkCore.Tools 3.1.19
    public class MoviesAppDbContext : DbContext
    {
        public MoviesAppDbContext(DbContextOptions<MoviesAppDbContext> options) : base(options) { }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // one to many relationship
            modelBuilder.Entity<Movie>()
                        .HasOne(x => x.User)
                        .WithMany(x => x.Movies)
                        .HasForeignKey(x => x.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
