using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Spot.Repositories.Generic
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext dbContext) => Context = dbContext;

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public async virtual Task<TEntity> GetAsync(TKey key)
        {
            return await Context.Set<TEntity>().FindAsync(key);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async virtual Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}