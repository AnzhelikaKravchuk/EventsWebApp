using EventsWebApp.Domain.Interfaces.Repositories;
using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class GenericRepository<T> : IBaseRepository<T> where T : IdModel
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }
        public async Task<Guid> Add(T model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _dbSet.AddAsync(model);

            return result.Entity.Id;
        }

        public async Task<int> Delete(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            int rowsDeleted = await _dbSet.Where(x => x.Id == id).ExecuteDeleteAsync();

            return rowsDeleted;
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> Update(T model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _dbSet.Update(model);
            return model.Id;
        }
    }
}
