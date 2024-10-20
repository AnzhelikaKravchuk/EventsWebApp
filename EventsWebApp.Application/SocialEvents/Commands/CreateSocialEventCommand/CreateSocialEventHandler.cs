using AutoMapper;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.SocialEvents.Commands
{
    public class CreateSocialEventHandler : ICommandHandler<CreateSocialEventCommand, Guid>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public CreateSocialEventHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSocialEventCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var socialEvent = _mapper.Map<SocialEvent>(request);
            var id = await _appUnitOfWork.SocialEventRepository.Add(socialEvent, cancellationToken);
            _appUnitOfWork.Save();
            return id;
        }
    }
}
