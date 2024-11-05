using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Helpers;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;

namespace EventsWebApp.Application.UseCases.SocialEvents.Queries
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
            Guid userId = TokenHelper.CheckToken(request.Token, _jwtProvider);
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetByIdWithInclude(request.Id, cancellationToken);
            if (socialEvent == null)
            {
                throw new NotFoundException("No social event was found");
            }

            cancellationToken.ThrowIfCancellationRequested();
            SocialEventDto socialEventDto = _mapper.Map<SocialEventDto>(socialEvent);
            Attendee attendee = socialEventDto.ListOfAttendees.FirstOrDefault((a) => a?.UserId == userId);
            socialEventDto.IsAlreadyInList = attendee != null;
            return socialEventDto;
        }
    }
}
