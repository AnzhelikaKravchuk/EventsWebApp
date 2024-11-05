using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Application.Interfaces.UseCases;
using EventsWebApp.Domain.Exceptions;
using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using System.Net;

namespace EventsWebApp.Application.UseCases.Users.Queries
{
    public class GetUserByEmailHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IMapper _mapper;
        public GetUserByEmailHandler(IAppUnitOfWork appUnitOfWork, IMapper mapper)
        {
            _appUnitOfWork = appUnitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            User user = await _appUnitOfWork.UserRepository.GetByEmail(request.Email, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new NotFoundException("No such user found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
