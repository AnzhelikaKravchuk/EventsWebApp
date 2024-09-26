using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<Guid> Add(Attendee attendee);
        Task<Guid> Delete(Guid id);
        Task<List<Attendee>> GetAll();
        Task<Attendee> GetById(Guid id);
        Task<Guid> Update(Attendee attendee);
    }
}