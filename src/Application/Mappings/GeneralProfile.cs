using Application.DTOs;
using Application.DTOs.Character;
using Application.DTOs.Gender;
using Application.DTOs.Movie;
using Application.Features.Characters.Commands.CreateCharacterCommand;
using Application.Features.Genders.Commands.CreateGenderCommand;
using Application.Features.Movies.Commands.CreateMovieCommand;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Character, CharacterListDto>();
            CreateMap<Character, CharacterDto>()
                .ForMember(dto => dto.Movies, m => m.MapFrom(m => m.CharacterMovies.Select(cm => cm.Movie)));
            CreateMap<Movie, MoviesCharacter>();


            CreateMap<Gender, GenderListDto>();
            CreateMap<Gender, GenderDto>();

            CreateMap<Movie, MovieListDto>();
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Characters, c => c.MapFrom(c => c.CharacterMovies.Select(cm => cm.Character)))
                .ForMember(dto => dto.Gender, g => g.MapFrom(g => g.Gender));
            CreateMap<Character, CharactersMovie>();
            CreateMap<Gender, GenderMovie>();
            #endregion


            #region Commands
            CreateMap<CreateCharacterCommand, Character>();
            CreateMap<CreateGenderCommand, Gender>();
            CreateMap<CreateMovieCommand, Movie>();
            #endregion
        }
    }
}


                