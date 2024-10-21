using EventsWebApp.Application.Interfaces.UseCases;
using MediatR;

namespace EventsWebApp.Application.ImageService.Commands
{
    public record DeleteImageCommand(string Path) : ICommand<Unit>;
}
