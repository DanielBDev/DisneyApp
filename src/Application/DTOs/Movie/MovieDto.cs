using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Movie
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        [Range(1, 5)]
        public int Qualification { get; set; }
        public string Image { get; set; }

        public GenderMovie Gender { get; set; }
        public ICollection<CharactersMovie> Characters { get; set; }
    }

    public class CharactersMovie
    {
        public string Name { get; set; }
    }

    public class GenderMovie
    {
        public string Name { get; set; }
    }
}
