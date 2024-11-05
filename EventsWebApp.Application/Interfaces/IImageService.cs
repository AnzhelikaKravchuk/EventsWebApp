using EventsWebApp.Application.Dto;

namespace EventsWebApp.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> StoreImage(StoreImageRequest  request, CancellationToken cancellationToken);
    }
}