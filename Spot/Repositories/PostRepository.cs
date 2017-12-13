using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Spot.Models.Generic.ViewModels;
using Spot.Models.Post;
using Spot.Models.Post.ViewModels;
using Spot.Repositories.Generic;

namespace Spot.Repositories
{
    public class PostRepository : Repository<int, PostModel>, IPostRepository
    {
        public DatabaseContext DatabaseContext => Context as DatabaseContext;

        public PostRepository(DatabaseContext context) : base(context) { }

        public async Task<PostEditViewModel> GetEditAsync(int id)
        {
            var result = await DatabaseContext.Posts
                .Select(p => new PostEditViewModel {
                    Id = p.Id,
                    Status = p.Status,
                    Title = p.Title,
                    Excerpt = p.Excerpt,
                    Content = p.Content
                })
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async Task<PostModel> GetAsync(int id, PostStatus? status = PostStatus.Public)
        {
            var query = DatabaseContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .Include(p => p.Category);

            if (status != null)
                query.Where(p => p.Status == status);

            return await query
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PagedViewModel<PostExcerptViewModel>> GetPagedAsync(int pageIndex, int pageSize, PostStatus? status = PostStatus.Public)
        {
            var query = DatabaseContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Select(p => new PostExcerptViewModel {
                    Id = p.Id,
                    Author = p.Author.UserName,
                    Status = p.Status,
                    Title = p.Title,
                    Excerpt = p.Excerpt,
                    Created = p.Created,
                    Modified = p.Modified,
                    Published = p.Published,
                    Category = p.Category
                });

            if (status != null)
                query = query.Where(p => p.Status == status);

            var total = await query.CountAsync();

            var result = await query
                .OrderByDescending(p => p.Published)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedViewModel<PostExcerptViewModel> {
                Pages = (total + pageSize - 1) / pageSize,
                Index = pageIndex,
                Entities = result
            };
        }
    }
}