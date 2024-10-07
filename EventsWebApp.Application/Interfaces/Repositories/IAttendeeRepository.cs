using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces.Repositories
{
    public interface IAttendeeRepository
    {
        Task<Attendee> Add(Attendee attendee);
        Task<Guid> Delete(Guid id);
        Task<List<Attendee>> GetAll();
        Task<List<Attendee>> GetAllByUserId(Guid userId);
        Task<Attendee> GetById(Guid id);
        Task<Guid> Update(Attendee attendee);
    }
}