using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EventsWebApp.Application.Services
{
    public class ImageService
    {
        public ImageService() { }

        public async Task<string> StoreImage(string webRootPath, IFormFile image)
        {
            if(image == null)
            {
                return string.Empty;
            }
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
            var filePath = Path.Combine(webRootPath, "images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Path.Combine("images", fileName);
        }

        public Task DeleteImage(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                }
            }catch (Exception) { }
            return Task.CompletedTask;
        }
    }
}
