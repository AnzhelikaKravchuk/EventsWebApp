namespace EventsWebApp.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        Task<T> GetById(Guid id, CancellationToken cancellationToken);
        Task<Guid> Add(T model, CancellationToken cancellationToken);
        Task<int> Delete(Guid id, CancellationToken cancellationToken);
        Task<Guid> Update(T model, CancellationToken cancellationToken);
    }
}
