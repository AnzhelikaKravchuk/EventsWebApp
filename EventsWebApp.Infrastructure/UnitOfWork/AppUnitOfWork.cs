using EventsWebApp.Application.Interfaces;

namespace EventsWebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;


        private bool disposed = false;

        public IUserRepository UserRepository { get { 
                return _userRepository; 
            } 
        }

        public AppUnitOfWork(ApplicationDbContext context, IUserRepository userRepository) {
            _context = context;
            _userRepository = userRepository;
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
