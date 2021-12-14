using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Database.Configuration;

namespace Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<CharacterMovie> CharacterMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new CharacterConfiguration(modelBuilder.Entity<Character>());
            new GenderConfiguration(modelBuilder.Entity<Gender>());
            new MovieConfiguration(modelBuilder.Entity<Movie>());
            new CharacterMovieConfiguration(modelBuilder.Entity<CharacterMovie>());
        }
    }
}
