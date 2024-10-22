using EventsWebApp.Application.Validators;

namespace EventsWebApp.Application.UseCases.Users.Commands
{
    public class DeleteUserValidator : IdValidator<DeleteUserCommand>
    {
        public DeleteUserValidator() : base()
        {
        }
    }
}
