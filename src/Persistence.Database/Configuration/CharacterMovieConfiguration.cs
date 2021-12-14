using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Database.Configuration
{
    public class CharacterMovieConfiguration
    {
        public CharacterMovieConfiguration(EntityTypeBuilder<CharacterMovie> builder)
        {
            builder.HasKey(cm => new { cm.CharacterId, cm.MovieId });

            builder.HasOne(c => c.Character)
                .WithMany(m => m.CharacterMovies)
                .HasForeignKey(c => c.CharacterId);

            builder.HasOne(m => m.Movie)
                .WithMany(c => c.CharacterMovies)
                .HasForeignKey(m => m.MovieId);
        }
    }
}
