using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.ValidationAttrubutes;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Characters.Commands.UpdateCharacterCommand
{
    public class UpdateCharacterCommand : IRequest<Response<int>>
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string History { get; set; }
        [FileExtensionAttribute(new[] { "image/jpg", "image/png" })]
        public IFormFile Image { get; set; }
    }

    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IFileStorage _fileStorage;

        public UpdateCharacterCommandHandler(ApplicationDbContext applicationDbContext, IFileStorage fileStorage)
        {
            _applicationDbContext = applicationDbContext;
            _fileStorage = fileStorage;
        }

        public async Task<Response<int>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _applicationDbContext.Characters.FirstOrDefaultAsync(c => c.CharacterId == request.CharacterId);

            if (character == null)
            {
                throw new KeyNotFoundException($"El registro '{request.CharacterId}' no fue encontrado.");
            }

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(character.Image))
                {
                    await _fileStorage.Delete(character.Image, AppConst.FileContainer.CharacterContainer);
                }
                string urlImg = await SaveImg(request.Image);
                character.Image = urlImg;
            }

            character.Name = request.Name;
            character.Age = request.Age;
            character.Weight = request.Weight;
            character.History = request.History;

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(request.CharacterId);
        }

        private async Task<string> SaveImg(IFormFile img)
        {
            using var stream = new MemoryStream();

            await img.CopyToAsync(stream);

            var fileBytes = stream.ToArray();

            return await _fileStorage.Create(fileBytes, img.ContentType, Path.GetExtension(img.FileName), AppConst.FileContainer.CharacterContainer, Guid.NewGuid().ToString());
        }
    }
}
