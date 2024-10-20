using EventsWebApp.Application.Validators;
using FluentValidation;

namespace EventsWebApp.Application.Users.Commands
{
    public class UpdateUserValidator : IdValidator<UpdateUserCommand>
    {
        public UpdateUserValidator() : base()
        {
            RuleFor(user => user.Password)
                .NotEmpty();

            RuleFor(user => user.Username)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
