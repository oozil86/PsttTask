using Microsoft.EntityFrameworkCore;
using PsttTask.Infrastucture;

namespace PsttTask.Infrastructure.Data
{
    public class GenericRepository<TEntity, TId> : Domain.Data.IGenericRepository<TEntity, TId>
        where TEntity : Entity
    {
        private readonly PsttTaskContext _dbContext;
        private DbSet<TEntity> _set;

        public GenericRepository(PsttTaskContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _set = _dbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            try
            {
                _set.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                _set.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _set.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbContext.AddRange(entities);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await _dbContext.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Remove(TId id)
        {
            var entity = _set.Find(id);
            if (entity == null) { return false; }
            try
            {
                _set.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveAsync(TId id)
        {
            var entity = await _set.FindAsync(id);
            if (entity == null) { return false; }
            try
            {
                _set.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {

            try
            {
                _set.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ReomveEntity(TEntity entity)
        {
            try
            {
                _set.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
