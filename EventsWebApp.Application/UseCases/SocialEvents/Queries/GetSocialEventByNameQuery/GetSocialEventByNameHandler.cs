using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
{
    public class GetSocialEventByNameHandler : IQueryHandler<GetSocialEventByNameQuery, SocialEventDto>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public GetSocialEventByNameHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<SocialEventDto> Handle(GetSocialEventByNameQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetByName(request.Name, cancellationToken);
            if (socialEvent == null)
            {
                throw new NotFoundException("No social event was found");
            }
            SocialEventDto socialEventDto = _mapper.Map<SocialEventDto>(socialEvent);
            return socialEventDto;
        }
    }
}
