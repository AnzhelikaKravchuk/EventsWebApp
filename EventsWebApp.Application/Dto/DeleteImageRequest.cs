using EventsWebApp.Application.Interfaces.UseCases;
using MediatR;

namespace EventsWebApp.Application.Dto
{
    public record DeleteImageRequest(string Path) : ICommand<Unit>;
}