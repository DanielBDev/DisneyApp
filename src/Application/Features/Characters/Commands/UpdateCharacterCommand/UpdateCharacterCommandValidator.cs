using FluentValidation;

namespace Application.Features.Characters.Commands.UpdateCharacterCommand
{
    public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
    {
        public UpdateCharacterCommandValidator()
        {
            RuleFor(k => k.CharacterId)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacio.");

            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio.")
                .MaximumLength(80).WithMessage("El campo Nombre no debe superar los {MaxLength} caracteres.");

            RuleFor(a => a.Age)
                .NotEmpty().WithMessage("El campo Edad no puede estar vacio.")
                .InclusiveBetween(1, 200);
            RuleFor(w => w.Weight)
                .NotEmpty().WithMessage("El campo Peso no puede estar vacio.")
                .ScalePrecision(2, 18);

            RuleFor(h => h.History)
                .NotEmpty().WithMessage("El campo Historia no puede estar vacio.");

            RuleFor(i => i.Image)
                .NotEmpty().WithMessage("Debe seleccionar una Imagen.");
        }
    }
}
