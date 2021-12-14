using Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genders.Commands.DeleteGenderCommand
{
    public class DeleteGenderCommand : IRequest<Response<int>>
    {
        public int GenderId { get; set; }
    }

    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteGenderCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = await _applicationDbContext.Genders.FirstOrDefaultAsync(g => g.GenderId == request.GenderId);

            if (gender == null)
            {
                throw new KeyNotFoundException($"El registro '{request.GenderId}' no fue encontrado.");
            }

            _applicationDbContext.Remove(gender);

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(gender.GenderId);
        }
    }
}
