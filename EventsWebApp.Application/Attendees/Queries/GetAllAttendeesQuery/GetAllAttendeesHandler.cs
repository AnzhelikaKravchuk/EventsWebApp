using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.Attendees.Queries
{
    public class GetAllAttendeesHandler : IQueryHandler<GetAllAttendeesQuery, List<AttendeeDto>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllAttendeesHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AttendeeDto>> Handle(GetAllAttendeesQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _appUnitOfWork.AttendeeRepository.GetAll(cancellationToken)).Select(_mapper.Map<AttendeeDto>).ToList();
        }
    }
}
