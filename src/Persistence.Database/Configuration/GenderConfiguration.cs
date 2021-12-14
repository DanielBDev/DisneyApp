using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Database.Configuration
{
    public class GenderConfiguration
    {
        public GenderConfiguration(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(k => k.GenderId);
            builder.Property(n => n.Name).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Image).IsRequired();
        }
    }
}
