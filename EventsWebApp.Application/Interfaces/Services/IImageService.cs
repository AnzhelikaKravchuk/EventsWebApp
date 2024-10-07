using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task DeleteImage(string filePath);
        Task<string> StoreImage(string webRootPath, IFormFile image);
    }
}