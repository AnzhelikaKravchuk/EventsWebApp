using EventsWebApp.Application.Interfaces.UseCases;
using MediatR;

namespace EventsWebApp.Application.UseCases.ImageService.Commands
{
    public class DeleteImageHandler : ICommandHandler<DeleteImageCommand, Unit>
    {
        public Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                FileInfo file = new FileInfo(request.Path);
                if (file.Exists)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    file.Delete();
                }
            }
            catch (Exception) { }
            return Task.FromResult(Unit.Value);
        }
    }
}
