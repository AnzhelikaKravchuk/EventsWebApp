using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using System.Net;

namespace EventsWebApp.Application.UseCases.Attendees.Queries
{
    public class GetAttendeesWithTokenHandler : IQueryHandler<GetAttendeesWithTokenQuery, List<AttendeeDto>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;

        public GetAttendeesWithTokenHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper, IJwtProvider jwtProvider)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<AttendeeDto>> Handle(GetAttendeesWithTokenQuery request, CancellationToken cancellationToken)
        {
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);

            cancellationToken.ThrowIfCancellationRequested();
            string userId = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                throw new InvalidTokenException("No user id found");
            }

            return (await _appUnitOfWork.AttendeeRepository.GetAllByUserId(Guid.Parse(userId), cancellationToken)).Select(_mapper.Map<AttendeeDto>).ToList();
        }
    }
}
