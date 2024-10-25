using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IAttendeeRepository : IBaseRepository<Attendee>
    {
        public Task<List<Attendee>> GetAllWithInclude(CancellationToken cancellationToken);
        public Task<Attendee> GetByIdWithInclude(Guid id, CancellationToken cancellationToken);
        Task<List<Attendee>> GetAllByUserId(Guid userId, CancellationToken cancellationToken);
    }
}