using System.Collections.Generic;
using System.Threading.Tasks;
using Spot.Models.Post;
using Spot.Repositories.Generic;

namespace Spot.Repositories
{
    public interface IPostRepository : IRepository<int, PostModel>
    {
        Task<PostModel> GetWithCommentsAsync(int id);

        Task<IEnumerable<PostModel>> GetPagedAsync(int pageIndex, int pageSize);

        Task<IEnumerable<PostModel>> GetPagedByTagAsync(int pageIndex, int pageSize, int tagId);

        Task<IEnumerable<PostModel>> GetPagedByAuthorAsync(int pageIndex, int pageSize, string author);
    }
}