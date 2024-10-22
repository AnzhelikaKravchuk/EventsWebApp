using EventsWebApp.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.UseCases.ImageService.Commands
{
    public record StoreImageCommand(string Path, IFormFile Image) : ICommand<string>;
}
