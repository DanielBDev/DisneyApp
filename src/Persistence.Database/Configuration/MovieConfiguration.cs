using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Database.Configuration
{
    public class MovieConfiguration
    {
        public MovieConfiguration(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(k => k.MovieId);
            builder.Property(t => t.Title).HasMaxLength(100).IsRequired();
            builder.Property(c => c.DateOfCreation).IsRequired();
            builder.Property(q => q.Qualification).HasMaxLength(1).HasPrecision(1,5).IsRequired();
            builder.Property(i => i.Image).IsRequired();

            builder.HasOne(g => g.Gender).WithMany(m => m.Movies).IsRequired();
        }
    }
}
