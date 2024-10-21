using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Users.Commands
{
    public class LoginUserValidator : LoginRegisterUserValidator<LoginUserCommand>
    {
        public LoginUserValidator() :base()
        {
        }
    }
}
