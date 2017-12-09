using System.Collections.Generic;
using System.Threading.Tasks;
using Spot.Models;
using Spot.Repositories.Generic;

namespace Spot.Repositories
{
    public interface IPostRepository : IRepository<int, Post>
    {
        Task<Post> GetWithCommentsAsync(int id);

        Task<IEnumerable<Post>> GetPagedAsync(int pageIndex, int pageSize);

        Task<IEnumerable<Post>> GetPagedByTagAsync(int pageIndex, int pageSize, int tagId);

        Task<IEnumerable<Post>> GetPagedByAuthorAsync(int pageIndex, int pageSize, string author);
    }
}