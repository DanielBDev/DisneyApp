using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileStorage
    {
        public Task<string> Create(byte[] file, string contentType, string extension, string container, string name);

        public Task Delete(string root, string container);
    }
}
