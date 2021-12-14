using FluentValidation;

namespace Application.Features.Genders.Commands.DeleteGenderCommand
{
    public class DeleteGenderCommandValidator : AbstractValidator<DeleteGenderCommand>
    {
        public DeleteGenderCommandValidator()
        {
            RuleFor(k => k.GenderId)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacio.");
        }
    }
}
