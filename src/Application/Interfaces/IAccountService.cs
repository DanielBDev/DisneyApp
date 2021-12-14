using Application.DTOs.Users;
using Application.Wrappers;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest authenticationRequest, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest registerRequest, string origin);
    }
}
