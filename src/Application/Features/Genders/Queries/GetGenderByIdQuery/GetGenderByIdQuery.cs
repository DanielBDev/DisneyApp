using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genders.Queries.GetGenderByIdQuery
{
    public class GetGenderByIdQuery : IRequest<Response<GenderDto>>
    {
        public int GenderId { get; set; }

        public class GetGenderByIdQueryHandler : IRequestHandler<GetGenderByIdQuery, Response<GenderDto>>
        {
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetGenderByIdQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<Response<GenderDto>> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken)
            {
                var gender = await _applicationDbContext.Genders.FirstOrDefaultAsync(x => x.GenderId == request.GenderId);

                if (gender == null)
                {
                    throw new KeyNotFoundException($"El registro '{request.GenderId}' no fue encontrado.");
                }

                var genderDto = _mapper.Map<GenderDto>(gender);

                return new Response<GenderDto>(genderDto);
            }
        }
    }
}
