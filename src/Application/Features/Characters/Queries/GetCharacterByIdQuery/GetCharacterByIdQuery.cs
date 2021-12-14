using Application.DTOs.Character;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Characters.Queries.GetCharacterByIdQuery
{
    public class GetCharacterByIdQuery : IRequest<Response<CharacterDto>>
    {
        public int CharacterId { get; set; }

        public class GetCharacterByIdQueryHandler : IRequestHandler<GetCharacterByIdQuery, Response<CharacterDto>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetCharacterByIdQueryHandler(IMapper mapper, ApplicationDbContext applicationDbContext)
            {
                _mapper = mapper;
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Response<CharacterDto>> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
            {
                var character = await _applicationDbContext.Characters
                    .Include(cm => cm.CharacterMovies)
                    .ThenInclude(m => m.Movie)
                    .FirstOrDefaultAsync(x => x.CharacterId == request.CharacterId);

                if (character == null)
                {
                    throw new KeyNotFoundException($"El registro '{request.CharacterId}' no fue encontrado.");
                }

                var characterDto = _mapper.Map<CharacterDto>(character);

                return new Response<CharacterDto>(characterDto);
            }
        }
    }    
}
