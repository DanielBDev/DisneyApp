using Application.DTOs.Gender;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genders.Queries.GetAllGendersQuery
{
    public class GetAllGendersQuery : IRequest<Response<List<GenderListDto>>>
    {
        public string Name { get; set; }

        public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, Response<List<GenderListDto>>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetAllGendersQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<Response<List<GenderListDto>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
            {
                if (request.Name != null)
                {
                    var gender = await _applicationDbContext.Genders.Where(x => x.Name.Contains(request.Name)).ToListAsync();

                    if (gender.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var genderDto = _mapper.Map<List<GenderListDto>>(gender);

                    return new Response<List<GenderListDto>>(genderDto);
                }
                else
                {
                    var gender = await _applicationDbContext.Genders.ToListAsync();

                    if (gender.Count() == 0)
                    {
                        throw new KeyNotFoundException($"No se encontraron datos.");
                    }

                    var genderDto = _mapper.Map<List<GenderListDto>>(gender);

                    return new Response<List<GenderListDto>>(genderDto);
                }
            }
        }
    }
}
