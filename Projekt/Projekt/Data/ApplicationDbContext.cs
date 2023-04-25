using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
           Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .HasOne(p => p.Producer)
                .WithMany(b => b.MoviesProduced)
                .HasForeignKey(p => p.ProducerId);
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<Actor> Actor { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Projekt; Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
