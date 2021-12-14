using Application.DTOs.Movie;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Movies.Queries.GetAllMoviesQuery
{
    public class GetAllMoviesQuery : IRequest<Response<List<MovieListDto>>>
    {
        public string Title { get; set; }
        public int Gender { get; set; }

        public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, Response<List<MovieListDto>>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetAllMoviesQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<Response<List<MovieListDto>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
            {
                if (request.Title != null)
                {
                    var movie = await _applicationDbContext.Movies.Where(t => t.Title.Contains(request.Title)).ToListAsync();

                    if (movie.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var movieDto = _mapper.Map<List<MovieListDto>>(movie);

                    return new Response<List<MovieListDto>>(movieDto);
                }
                else if (request.Gender != 0)
                {
                    var movie = await _applicationDbContext.Movies.Where(t => t.IdGender == request.Gender).ToListAsync();

                    if (movie.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var movieDto = _mapper.Map<List<MovieListDto>>(movie);

                    return new Response<List<MovieListDto>>(movieDto);
                }
                else
                {
                    var movie = await _applicationDbContext.Movies.ToListAsync();

                    if (movie.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var movieDto = _mapper.Map<List<MovieListDto>>(movie);

                    return new Response<List<MovieListDto>>(movieDto);
                }                
            }
        }
    }
}
