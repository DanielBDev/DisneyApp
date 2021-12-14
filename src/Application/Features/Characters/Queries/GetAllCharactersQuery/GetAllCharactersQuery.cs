using Application.DTOs.Character;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Characters.Queries.GetAllCharactersQuery
{
    public class GetAllCharactersQuery : IRequest<Response<List<CharacterListDto>>>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public class GetAllCharacterQueryHandler : IRequestHandler<GetAllCharactersQuery, Response<List<CharacterListDto>>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetAllCharacterQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<Response<List<CharacterListDto>>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
            {
                if (request.Name != null)
                {
                    var character = await _applicationDbContext.Characters.Where(x => x.Name.Contains(request.Name)).ToListAsync();

                    if (character.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var characterDto = _mapper.Map<List<CharacterListDto>>(character);

                    return new Response<List<CharacterListDto>>(characterDto);
                }
                else if(request.Age != 0)
                {
                    var character = await _applicationDbContext.Characters.Where(x => x.Age >= request.Age).ToListAsync();

                    if (character.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var characterDto = _mapper.Map<List<CharacterListDto>>(character);

                    return new Response<List<CharacterListDto>>(characterDto);
                }
                else
                {
                    var character = await _applicationDbContext.Characters.ToListAsync();

                    if (character.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var characterDto = _mapper.Map<List<CharacterListDto>>(character);

                    return new Response<List<CharacterListDto>>(characterDto);
                }
            }
        }
    }
}
