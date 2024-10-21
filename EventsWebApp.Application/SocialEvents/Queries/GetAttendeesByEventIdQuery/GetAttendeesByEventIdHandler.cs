using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.SocialEvents.Queries
{
    public class GetAttendeesByEventIdHandler : IQueryHandler<GetAttendeesByEventIdQuery, List<AttendeeDto>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public GetAttendeesByEventIdHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AttendeeDto>> Handle(GetAttendeesByEventIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SocialEvent socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(request.Id, cancellationToken);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }

            List<AttendeeDto> responseList = socialEvent.ListOfAttendees.Select(_mapper.Map<AttendeeDto>).ToList();
            return responseList;
        }
    }
}
