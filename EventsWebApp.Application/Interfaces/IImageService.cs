using EventsWebApp.Application.Dto;
using MediatR;

namespace EventsWebApp.Application.Interfaces
{
    public interface IImageService
    {
        Task<Unit> DeleteImage(DeleteImageRequest request, CancellationToken cancellationToken);
        Task<string> StoreImage(StoreImageRequest  request, CancellationToken cancellationToken);
    }
}