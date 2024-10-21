using EventsWebApp.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.ImageService.Commands
{
    public record StoreImageCommand(string Path, IFormFile Image) : ICommand<string>;
}
