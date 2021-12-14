using Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Characters.Commands.DeleteCharacterCommand
{
    public class DeleteCharacterCommand : IRequest<Response<int>>
    {
        public int CharacterId { get; set; }
    }

    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteCharacterCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _applicationDbContext.Characters.FirstOrDefaultAsync(c => c.CharacterId == request.CharacterId);

            if (character == null)
            {
                throw new KeyNotFoundException($"El registro '{request.CharacterId}' no fue encontrado.");
            }

            _applicationDbContext.Remove(character);

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(character.CharacterId);
        }
    }
}
