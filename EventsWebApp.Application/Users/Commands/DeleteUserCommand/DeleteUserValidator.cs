using FluentValidation;

namespace EventsWebApp.Application.Users.Commands.DeleteUserCommand
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(user => user.Id)
                .NotEmpty();
        }
    }
}
