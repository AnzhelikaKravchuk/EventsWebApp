using FluentValidation;

namespace EventsWebApp.Application.Users.Commands
{
    public class RefreshTokenValidator :AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator() {
            RuleFor(request => request.AccessToken)
                .NotEmpty();

            RuleFor(request => request.RefreshToken)
                .NotEmpty();
        }
    }
}
