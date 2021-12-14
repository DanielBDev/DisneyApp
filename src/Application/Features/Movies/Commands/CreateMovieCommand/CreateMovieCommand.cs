using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.ValidationAttrubutes;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Movies.Commands.CreateMovieCommand
{
    public class CreateMovieCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        [Range(1, 5)]
        public int Qualification { get; set; }
        [FileExtensionAttribute(new[] { "image/jpg", "image/png" })]
        public IFormFile Image { get; set; }
        public int IdGender { get; set; }
        public ICollection<int> CharactersIds { get; set; }

        public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Response<int>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            private readonly IFileStorage _fileStorage;

            public CreateMovieCommandHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
                _fileStorage = fileStorage;
            }

            public async Task<Response<int>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
            {
                var newRecord = _mapper.Map<Movie>(request);

                if (request.Image != null)
                {
                    string urlImg = await SaveImg(request.Image);
                    newRecord.Image = urlImg;
                }

                await _applicationDbContext.AddAsync(newRecord);

                await _applicationDbContext.SaveChangesAsync();

                foreach (var id in request.CharactersIds)
                {
                    var characterMovies = new CharacterMovie()
                    {
                        CharacterId = id,
                        MovieId = newRecord.MovieId
                    };

                    await _applicationDbContext.CharacterMovies.AddAsync(characterMovies);
                    await _applicationDbContext.SaveChangesAsync();
                }

                return new Response<int>(newRecord.MovieId);
            }

            private async Task<string> SaveImg(IFormFile img)
            {
                using var stream = new MemoryStream();

                await img.CopyToAsync(stream);

                var fileBytes = stream.ToArray();

                return await _fileStorage.Create(fileBytes, img.ContentType, Path.GetExtension(img.FileName), AppConst.FileContainer.MovieContainer, Guid.NewGuid().ToString());
            }
        }
    }
}
