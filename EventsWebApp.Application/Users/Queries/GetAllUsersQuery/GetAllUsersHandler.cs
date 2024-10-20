using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Interfaces.Repositories;

namespace EventsWebApp.Application.Users.Queries
{
    public class GetAllUsersHandler : IQueryHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper; 
        public GetAllUsersHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _appUnitOfWork.UserRepository.GetAll(cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            var usersDtos = users.ConvertAll(_mapper.Map<UserDto>).ToList();
            return usersDtos;
        }
    }
}
