
namespace EventsWebApp.Application.Interfaces
{
    public interface IAppUnitOfWork
    {
        IUserRepository UserRepository { get; }

        void Dispose();
        void Save();
    }
}