using Application.Extensions;
using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.ValidationAttrubutes;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Movies.Commands.UpdateMovieCommand
{
    public class UpdateMovieCommand : IRequest<Response<int>>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        [Range(1, 5)]
        public int Qualification { get; set; }
        [FileExtensionAttribute(new[] { "image/jpg", "image/png" })]
        public IFormFile Image { get; set; }
        public int IdGender { get; set; }
        public ICollection<int> CharactersIds { get; set; }
    }

    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IFileStorage _fileStorage;

        public UpdateMovieCommandHandler(ApplicationDbContext applicationDbContext, IFileStorage fileStorage)
        {
            _applicationDbContext = applicationDbContext;
            _fileStorage = fileStorage;
        }

        public async Task<Response<int>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _applicationDbContext.Movies
                .Include(cm => cm.CharacterMovies)
                .FirstOrDefaultAsync(x => x.MovieId == request.MovieId);

            if (movie == null)
            {
                throw new KeyNotFoundException($"El registro '{request.MovieId}' no fue encontrado.");
            }

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(movie.Image))
                {
                    await _fileStorage.Delete(movie.Image, AppConst.FileContainer.CharacterContainer);
                }
                string urlImg = await SaveImg(request.Image);
                movie.Image = urlImg;
            }

            movie.Title = request.Title;
            movie.DateOfCreation = request.DateOfCreation;
            movie.Qualification = request.Qualification;
            movie.IdGender = request.IdGender;

            _applicationDbContext.TryUpdateManyToMany(movie.CharacterMovies, request.CharactersIds
                .Select(x => new CharacterMovie 
                {
                    CharacterId = x,
                    MovieId = request.MovieId
                }), x => x.CharacterId);

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(request.MovieId);
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







