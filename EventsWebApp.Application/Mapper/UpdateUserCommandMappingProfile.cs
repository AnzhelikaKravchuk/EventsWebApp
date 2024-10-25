using AutoMapper;
using EventsWebApp.Application.UseCases.Users.Commands;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Mapper
{
    public class UpdateUserCommandMappingProfile : Profile
    {
        public UpdateUserCommandMappingProfile() {
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
