using System.Collections.Generic;

namespace Domain.Entities
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string History { get; set; }
        public string Image { get; set; }

        public virtual ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}
