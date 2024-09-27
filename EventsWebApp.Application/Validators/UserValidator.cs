using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            //RuleFor(user => user.Id).NotEmpty();
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Role).NotEmpty();
        }
    }
}
