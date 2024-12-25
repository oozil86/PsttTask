using PsttTask.Domain.Contracts;

namespace PsttTask.Domain.Data
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        bool Remove(TId id);
        Task<bool> RemoveAsync(TId id);
        void RemoveRange(IEnumerable<TEntity> entities);
        void ReomveEntity(TEntity entity);
    }
}
