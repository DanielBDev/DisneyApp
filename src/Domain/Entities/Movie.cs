using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        [Range(1, 5)]
        public int Qualification { get; set; }
        public string Image { get; set; }

        public int IdGender { get; set; }
        [ForeignKey(nameof(IdGender))]
        public Gender Gender { get; set; }

        public virtual ICollection<CharacterMovie> CharacterMovies { get; set; }
    }
}
