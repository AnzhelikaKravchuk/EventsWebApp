using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
    }
}
