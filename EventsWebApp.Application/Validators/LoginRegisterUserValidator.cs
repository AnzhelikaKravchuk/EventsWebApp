using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class LoginRegisterUserValidator<T> : AbstractValidator<T> where T : LoginRegisterUserRequest
    {
        public LoginRegisterUserValidator() {
            RuleFor(user => user.Email)
                .NotEmpty()
                .MaximumLength(100)
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}
