using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public class GetSocialEventByIdHandler : IQueryHandler<GetSocialEventByIdQuery, SocialEventDto>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public GetSocialEventByIdHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<SocialEventDto> Handle(GetSocialEventByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetByIdWithInclude(request.Id, cancellationToken);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }
            SocialEventDto socialEventDto = _mapper.Map<SocialEventDto>(socialEvent);
            return socialEventDto;
        }
    }
}
