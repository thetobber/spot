using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Spot.Repositories.Generic
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, new()
    {
        protected readonly DbContext Context;

        public Repository(DbContext dbContext) => Context = dbContext;

        public virtual void Add(TEntity entity) =>
            Context.Set<TEntity>().Add(entity);

        public virtual void AddRange(IEnumerable<TEntity> entities) =>
            Context.Set<TEntity>().AddRange(entities);

        public async virtual Task<TEntity> GetAsync(TKey key) =>
            await Context.Set<TEntity>().FindAsync(key);

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync() =>
            await Context.Set<TEntity>().ToListAsync();

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Set<TEntity>().Remove(entity);
            //Context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);

            foreach (TEntity entity in entities)
                Context.Entry(entity).State = EntityState.Deleted;
        }

        public async virtual Task<int> SaveAsync() =>
            await Context.SaveChangesAsync();
    }
}