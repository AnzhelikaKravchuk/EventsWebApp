using FluentValidation;

namespace EventsWebApp.Application.Users.Commands.RegisterUserCommand
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator() {
            RuleFor(user => user.Email)
                .NotEmpty()
                .MaximumLength(100)
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty();

            RuleFor(user => user.Username)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
