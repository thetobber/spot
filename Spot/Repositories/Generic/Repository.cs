using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Spot.Repositories.Generic
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext dbContext) => Context = dbContext;

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public async Task<TEntity> GetAsync(TKey key)
        {
            return await Context.Set<TEntity>().FindAsync(key);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}