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
        Task<PostEditViewModel> GetEditAsync(int id);

        Task<PagedViewModel<PostExcerptViewModel>> GetPagedAsync(int pageIndex = 1, int pageSize = 10);
    }
}