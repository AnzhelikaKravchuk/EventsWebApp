
using Microsoft.AspNetCore.Http;

namespace EventsWebApp.Application.Dto
{
    public class StoreImageRequest
    {
        public string Path { get; set; }
        public IFormFile Image { get; set; }
        public StoreImageRequest()
        {
            
        }

        public StoreImageRequest(string path, IFormFile formFile)
        {
            Path = path;
            Image = formFile;
        }
    }
}
