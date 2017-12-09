using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spot.Repositories.Generic
{
    public interface IRepository<TKey, TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(TKey key);

        Task<IEnumerable<TEntity>> GetAllAsync();

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> Save();
    }
}