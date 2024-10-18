using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IAttendeeRepository : IBaseRepository<Attendee>
    {
        Task<List<Attendee>> GetAllByUserId(Guid userId);
    }
}