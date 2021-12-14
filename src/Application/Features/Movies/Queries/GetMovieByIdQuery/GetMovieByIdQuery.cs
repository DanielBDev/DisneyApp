using Application.DTOs.Movie;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Movies.Queries.GetMovieByIdQuery
{
    public class GetMovieByIdQuery : IRequest<Response<MovieDto>>
    {
        public int MovieId { get; set; }

        public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Response<MovieDto>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetMovieByIdQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<Response<MovieDto>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
            {
                var movie = await _applicationDbContext.Movies
                    .Include(g => g.Gender)
                    .Include(cm => cm.CharacterMovies)
                    .ThenInclude(c => c.Character)
                    .FirstOrDefaultAsync(x => x.MovieId == request.MovieId);

                if (movie == null)
                {
                    throw new KeyNotFoundException($"El registro '{request.MovieId}' no fue encontrado.");
                }

                var movieDto = _mapper.Map<MovieDto>(movie);

                return new Response<MovieDto>(movieDto);
            }
        }
    }
}
