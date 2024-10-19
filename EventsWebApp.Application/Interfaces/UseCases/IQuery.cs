using MediatR;

namespace EventsWebApp.Application.Interfaces.UseCases
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
