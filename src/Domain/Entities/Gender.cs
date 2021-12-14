using System.Collections.Generic;

namespace Domain.Entities
{
    public class Gender
    {
        public int GenderId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
