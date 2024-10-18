namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<Guid> Add(T model);
        Task<int> Delete(Guid id);
        Task<Guid> Update(T model);
    }
}
