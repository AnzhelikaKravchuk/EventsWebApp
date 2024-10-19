﻿using FluentValidation;

namespace EventsWebApp.Application.Users.Commands.LoginUserCommand
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .MaximumLength(100)
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}
