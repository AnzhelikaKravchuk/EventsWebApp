using AutoMapper;
using EventsWebApp.Application.UseCases.Users.Commands;
using EventsWebApp.Domain.Enums;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class RegisterUserCommandMappingProfile : Profile
    {
        public RegisterUserCommandMappingProfile() {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .AfterMap((com, user) => user.Role = E_Role.User)
                ;
        }
    }
}
