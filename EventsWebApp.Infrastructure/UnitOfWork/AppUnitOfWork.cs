using EventsWebApp.Application.Interfaces.Repositories;

namespace EventsWebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IDisposable, IAppUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;
        private ISocialEventRepository _socialEventRepository;
        private IAttendeeRepository _attendeeRepository;


        private bool disposed = false;

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        public ISocialEventRepository SocialEventRepository
        {
            get
            {
                return _socialEventRepository;
            }
        }

        public IAttendeeRepository AttendeeRepository
        {
            get
            {
                return _attendeeRepository;
            }
        }
        public AppUnitOfWork(ApplicationDbContext context, IUserRepository userRepository, ISocialEventRepository socialEventRepository, IAttendeeRepository attendeeRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _socialEventRepository = socialEventRepository;
            _attendeeRepository = attendeeRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
