using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(user => user.Id).NotEmpty();
            RuleFor(user => user.Email).NotNull().EmailAddress();
            RuleFor(user => user.Username).NotNull();
            RuleFor(user => user.Role).NotNull();
        }
    }
}
