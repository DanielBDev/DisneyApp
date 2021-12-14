using FluentValidation;

namespace Application.Features.Characters.Commands.CreateCharacterCommand
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacío.")
                .MaximumLength(80).WithMessage("El campo Nombre no debe superar los {MaxLength} caracteres.");

            RuleFor(a => a.Age)
                .NotEmpty().WithMessage("El campo Edad no puede estar vacío.")
                .InclusiveBetween(1,200);
            RuleFor(w => w.Weight)
                .NotEmpty().WithMessage("El campo Peso no puede estar vacío.")
                .ScalePrecision(2, 18);

            RuleFor(h => h.History)
                .NotEmpty().WithMessage("El campo Historia no puede estar vacío.");

            RuleFor(i => i.Image)
                .NotEmpty().WithMessage("Debe seleccionar una Imagen.");
        }
    }
}