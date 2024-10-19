using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.SocialEvents.Queries.GetAttendeesByEventIdQuery
{
    public class GetAttendeesByEventIdHandler : IQueryHandler<GetAttendeesByEventIdQuery, List<AttendeeResponse>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public GetAttendeesByEventIdHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AttendeeResponse>> Handle(GetAttendeesByEventIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var socialEvent = await _appUnitOfWork.SocialEventRepository.GetById(request.Id, cancellationToken);
            if (socialEvent == null)
            {
                throw new SocialEventException("No social event was found");
            }

            List<AttendeeResponse> responseList = socialEvent.ListOfAttendees.Select(_mapper.Map<AttendeeResponse>).ToList();
            return responseList;
        }
    }
}
