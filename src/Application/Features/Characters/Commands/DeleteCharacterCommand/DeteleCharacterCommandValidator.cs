using FluentValidation;

namespace Application.Features.Characters.Commands.DeleteCharacterCommand
{
    public class DeteleCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
    {
        public DeteleCharacterCommandValidator()
        {
            RuleFor(k => k.CharacterId)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacio.");
        }
    }
}
