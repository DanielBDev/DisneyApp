using FluentValidation;

namespace Application.Features.Genders.Commands.CreateGenderCommand
{
    public class CreateGenderCommandValidator : AbstractValidator<CreateGenderCommand>
    {
        public CreateGenderCommandValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacío.")
                .MaximumLength(80).WithMessage("El campo Nombre no debe superar los {MaxLength} caracteres.");

            RuleFor(i => i.Image)
                .NotEmpty().WithMessage("Debe seleccionar una Imagen.");
        }
    }
}
