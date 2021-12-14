using FluentValidation;

namespace Application.Features.Movies.Commands.CreateMovieCommand
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("El campo Título no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo Título no debe superar los {MaxLength} caracteres.");

            RuleFor(d => d.DateOfCreation)
                .NotEmpty().WithMessage("El campo Fecha no puede estar vacío.");

            RuleFor(q => q.Qualification)
                .NotEmpty().WithMessage("El campo Calificación no puede estar vacío.")
                .InclusiveBetween(1, 5).WithMessage("El campo Calificacíon solo puede aceptar un valor del 1-5.");

            RuleFor(i => i.Image)
                .NotEmpty().WithMessage("Debe seleccionar una Imagen.");

            RuleFor(g => g.IdGender)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío.");

            RuleFor(c => c.CharactersIds)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacío.");
        }
    }
}
