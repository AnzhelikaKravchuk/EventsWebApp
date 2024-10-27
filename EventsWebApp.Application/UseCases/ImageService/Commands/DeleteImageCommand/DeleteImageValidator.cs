using FluentValidation;

namespace EventsWebApp.Application.UseCases.ImageService.Commands
{
    public class DeleteImageValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageValidator() {
            RuleFor(x => x.Path)
                .NotEmpty();
        }
    }
}
