using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.ValidationAttrubutes;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Database;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genders.Commands.CreateGenderCommand
{
    public class CreateGenderCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        [FileExtensionAttribute(new[] { "image/jpg", "image/png" })]
        public IFormFile Image { get; set; }
    }

    public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, Response<int>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public CreateGenderCommandHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<Response<int>> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<Gender>(request);

            if (request.Image != null)
            {
                string urlImg = await SaveImg(request.Image);
                newRecord.Image = urlImg;
            }

            await _applicationDbContext.AddAsync(newRecord);

            await _applicationDbContext.SaveChangesAsync();

            return new Response<int>(newRecord.GenderId);
        }

        private async Task<string> SaveImg(IFormFile img)
        {
            using var stream = new MemoryStream();

            await img.CopyToAsync(stream);

            var fileBytes = stream.ToArray();

            return await _fileStorage.Create(fileBytes, img.ContentType, Path.GetExtension(img.FileName), AppConst.FileContainer.GenderContainer, Guid.NewGuid().ToString());
        }
    }
}
