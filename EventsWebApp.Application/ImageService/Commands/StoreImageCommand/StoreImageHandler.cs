using EventsWebApp.Application.Interfaces.UseCases;

namespace EventsWebApp.Application.ImageService.Commands
{
    public class StoreImageHandler : ICommandHandler<StoreImageCommand, string>
    {
        public async Task<string> Handle(StoreImageCommand request, CancellationToken cancellationToken)
        {
            if (request.Image == null)
            {
                return string.Empty;
            }
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(request.Image.FileName)}";
            var filePath = Path.Combine(request.Path, "images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            return Path.Combine("images", fileName);
        }
    }
}
