using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile() {

            CreateMap<User, UserDto>();
        }
    }
}
