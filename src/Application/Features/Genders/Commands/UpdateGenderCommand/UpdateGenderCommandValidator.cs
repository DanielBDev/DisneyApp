using FluentValidation;

namespace Application.Features.Genders.Commands.UpdateGenderCommand
{
    public class UpdateGenderCommandValidator : AbstractValidator<UpdateGenderCommand>
    {
        public UpdateGenderCommandValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio.")
                .MaximumLength(80).WithMessage("El campo Nombre no debe superar los {MaxLength} caracteres.");

            RuleFor(i => i.Image)
                .NotEmpty().WithMessage("Debe seleccionar una Imagen.");
        }
    }
}
