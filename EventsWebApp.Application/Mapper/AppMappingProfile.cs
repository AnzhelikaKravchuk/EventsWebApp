using AutoMapper;
using EventsWebApp.Application.Dto;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() {
            CreateMap<User, UserDto>();
        }
    }
}
