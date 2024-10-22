using EventsWebApp.Application.Helpers;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using MediatR;

namespace EventsWebApp.Application.UseCases.Attendees.Commands
{
    public class AddAttendeeWithTokenHandler : ICommandHandler<AddAttendeeWithTokenCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IJwtProvider _jwtProvider;
        public AddAttendeeWithTokenHandler(IMediator mediator, IJwtProvider jwtProvider)
        {
            _mediator = mediator;
            _jwtProvider = jwtProvider;
        }

        public async Task<Guid> Handle(AddAttendeeWithTokenCommand request, CancellationToken cancellationToken)
        {
            Guid userId = TokenHelper.CheckToken(request.AccessToken, _jwtProvider);
            return await _mediator.Send(new AddAttendeeCommand(request.Name, request.Surname, request.Email, request.DateOfBirth, request.EventId, userId), cancellationToken);
        }
    }
}
