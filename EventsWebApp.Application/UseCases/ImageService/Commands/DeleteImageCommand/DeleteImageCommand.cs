using EventsWebApp.Application.Interfaces.UseCases;
using MediatR;

namespace EventsWebApp.Application.UseCases.ImageService.Commands
{
    public record DeleteImageCommand(string Path) : ICommand<Unit>;
}