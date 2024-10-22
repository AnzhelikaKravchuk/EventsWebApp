using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.UseCases.Attendees.Queries
{
    internal class GetAttendeeByIdHandler : IQueryHandler<GetAttendeeByIdQuery, AttendeeDto>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;

        public GetAttendeeByIdHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<AttendeeDto> Handle(GetAttendeeByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _mapper.Map<AttendeeDto>(await _appUnitOfWork.AttendeeRepository.GetById(request.Id, cancellationToken));
        }
    }
}
