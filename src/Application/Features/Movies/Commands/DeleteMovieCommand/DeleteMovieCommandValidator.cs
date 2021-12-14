using FluentValidation;

namespace Application.Features.Movies.Commands.DeleteMovieCommand
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(k => k.MovieId)
                .NotEmpty().WithMessage("El campo {PropertyName} no puede estar vacio.");
        }
    }
}
