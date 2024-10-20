using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.Users.Commands
{
    public class DeleteUserValidator : IdValidator<DeleteUserCommand>
    {
        public DeleteUserValidator() : base()
        {
        }
    }
}
