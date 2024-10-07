namespace EventsWebApp.Application.Interfaces.Repositories
{
    public interface IAppUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ISocialEventRepository SocialEventRepository { get; }
        IAttendeeRepository AttendeeRepository { get; }

        void Dispose();
        void Save();
    }
}