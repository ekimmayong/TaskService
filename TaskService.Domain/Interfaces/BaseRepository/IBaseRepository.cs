

namespace TaskService.Domain.Interfaces.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int Id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, byte[] rowVersion);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void SetModified(object entity);
    }
}
