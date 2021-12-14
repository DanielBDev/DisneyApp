using FluentValidation;

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío.")
                .EmailAddress().WithMessage("{PropertyValue} no es un email válido.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Ingrese una contraseña.");
        }
    }
}
