using System.Collections.Generic;

namespace Application.DTOs.Character
{
    public class CharacterDto
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string History { get; set; }
        public string Image { get; set; }

        public ICollection<MoviesCharacter> Movies { get; set; }
    }

    public class MoviesCharacter
    {
        public string Title { get; set; }
    }
}
