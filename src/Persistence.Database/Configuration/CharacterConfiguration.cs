using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Database.Configuration
{
    public class CharacterConfiguration
    {
        public CharacterConfiguration(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(k => k.CharacterId);
            builder.Property(n => n.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Age).HasMaxLength(4).IsRequired();
            builder.Property(w => w.Weight).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(h => h.History).IsRequired();
            builder.Property(i => i.Image).IsRequired();        
        }
    }
}
