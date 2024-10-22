using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public class LoginUserValidator : LoginRegisterUserValidator<LoginUserCommand>
    {
        public LoginUserValidator() : base()
        {
        }
    }
}
