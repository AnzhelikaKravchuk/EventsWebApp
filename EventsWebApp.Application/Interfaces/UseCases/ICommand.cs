using MediatR;

namespace EventsWebApp.Application.Interfaces.UseCases
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
