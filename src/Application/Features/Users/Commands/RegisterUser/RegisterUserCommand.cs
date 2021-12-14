using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IAccountService _accountService;
        private readonly IMailService _mailService;

        public RegisterUserCommandHandler(IAccountService accountService, IMailService mailService)
        {
            _accountService = accountService;
            _mailService = mailService;
        }

        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _accountService.RegisterAsync(new RegisterRequest
            {
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            }, request.Origin);

            await _mailService.SendEmailAsync(request.Email, "Bienvenido/a", $"<h1>Hola {request.FirstName}!</h1><p>Bienvenido/a a DisneyApp, muchas gracias por elegirnos.</p>");
            
            return result;
        }
    }
}
