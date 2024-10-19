using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Helpers;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.SocialEvents.Queries.GetSocialEventByUserWithTokenQuery
{
    public class GetSocialEventByUserWithTokenHandler : IQueryHandler<GetSocialEventByUserWithTokenQuery, SocialEventDto>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public GetSocialEventByUserWithTokenHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper, IJwtProvider jwtProvider)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        public async Task<SocialEventDto> Handle(GetSocialEventByUserWithTokenQuery request, CancellationToken cancellationToken)
        {
            var userId = TokenHelper.CheckToken(request.Token, _jwtProvider);
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(request.Id, cancellationToken);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }

            cancellationToken.ThrowIfCancellationRequested();
            var socialEventDto = _mapper.Map<SocialEventDto>(socialEvent);
            var attendee = socialEventDto.ListOfAttendees.FirstOrDefault((a) => a.UserId == userId);
            socialEventDto.IsAlreadyInList = attendee != null;
            return socialEventDto;
        }
    }
}
