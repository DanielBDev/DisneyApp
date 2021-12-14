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

namespace Application.Features.Genders.Commands.UpdateGenderCommand
{
    public class UpdateGenderCommand : IRequest<Response<int>>
    {
        public int GenderId { get; set; }
        public string Name { get; set; }
        [FileExtensionAttribute(new[] { "image/jpg", "image/png" })]
        public IFormFile Image { get; set; }
    }

    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IFileStorage _fileStorage;

        public UpdateGenderCommandHandler(ApplicationDbContext applicationDbContext, IFileStorage fileStorage)
        {
            _applicationDbContext = applicationDbContext;
            _fileStorage = fileStorage;
        }

        public async Task<Response<int>> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = await _applicationDbContext.Genders.FirstOrDefaultAsync(g => g.GenderId == request.GenderId);

            if (gender == null)
            {
                throw new KeyNotFoundException($"El registro '{request.GenderId}' no fue encontrado.");
            }

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(gender.Image))
                {
                    await _fileStorage.Delete(gender.Image, AppConst.FileContainer.CharacterContainer);
                }
                string urlImg = await SaveImg(request.Image);
                gender.Image = urlImg;
            }

            gender.Name = request.Name;

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(request.GenderId);
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