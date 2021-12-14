using Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Movies.Commands.DeleteMovieCommand
{
    public class DeleteMovieCommand : IRequest<Response<int>>
    {
        public int MovieId { get; set; }
    }

    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteMovieCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _applicationDbContext.Movies
                .Include(cm => cm.CharacterMovies)
                .FirstOrDefaultAsync(x => x.MovieId == request.MovieId);

            if (movie == null)
            {
                throw new KeyNotFoundException($"El registro '{request.MovieId}' no fue encontrado.");
            }

            _applicationDbContext.Movies.Remove(movie);

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(movie.MovieId);
        }
    }
}
