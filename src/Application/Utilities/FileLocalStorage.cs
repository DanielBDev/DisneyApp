using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Utilities
{
    public class FileLocalStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public FileLocalStorage(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccesor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task<string> Create(byte[] file, string contentType, string extension, string container, string name)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;

            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }

            string fileFolder = Path.Combine(wwwrootPath, container);

            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }

            string finalName = $"{name}{extension}";

            string finalRoute = Path.Combine(fileFolder, finalName);

            await File.WriteAllBytesAsync(finalRoute, file);

            string currentUrl = $"{_httpContextAccesor.HttpContext.Request.Scheme}://{_httpContextAccesor.HttpContext.Request.Host}";

            string dbUrl = Path.Combine(currentUrl, container, finalName).Replace("\\","/");

            return dbUrl;
        }

        public Task Delete(string root, string container)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;

            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }

            var fileName = Path.GetFileName(root);

            string finalPath = Path.Combine(wwwrootPath, container, fileName);

            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }

            return Task.CompletedTask;
        }
    }
}
