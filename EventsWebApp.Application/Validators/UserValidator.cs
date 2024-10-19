using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Models;
using FluentValidation;
using System.Text;

namespace EventsWebApp.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.PasswordHash).NotEmpty();
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Role).NotEmpty();
        }

        public void ValidateUser(User user)
        {
            var result = Validate(user);
            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
                throw new UserException(stringBuilder.ToString());
            }
        }
    }
}
