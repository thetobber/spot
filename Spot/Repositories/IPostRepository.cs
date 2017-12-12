using System.Collections.Generic;
using System.Threading.Tasks;
using Spot.Models.Generic.ViewModels;
using Spot.Models.Post;
using Spot.Models.Post.ViewModels;
using Spot.Repositories.Generic;

namespace Spot.Repositories
{
    public interface IPostRepository : IRepository<int, PostModel>
    {
        Task<PostModel> GetWithCommentsAsync(int id);

        Task<PagedViewModel<PostExcerptViewModel>> GetPagedAsync(int pageIndex = 1, int pageSize = 10);

        Task<IEnumerable<PostModel>> GetPagedByTagAsync(int pageIndex, int pageSize, int tagId);

        Task<IEnumerable<PostModel>> GetPagedByAuthorAsync(int pageIndex, int pageSize, string author);

        Task<int> GetTotalCount();
    }
}