using FluentValidation;

namespace EventsWebApp.Application.Users.Commands.UpdateUserCommand
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Id)
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotEmpty();

            RuleFor(user => user.Username)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
