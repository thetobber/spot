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

        Task<PostModel> GetAsync(int id, PostStatus? status = PostStatus.Public);

        Task<PagedViewModel<PostExcerptViewModel>> GetPagedAsync(int pageIndex, int pageSize, PostStatus? status = PostStatus.Public);
    }
}