
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
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

        public async Task<PagedViewModel<PostExcerptViewModel>> GetPagedAsync(int pageIndex = 1, int pageSize = 10)
        {
            var query = DatabaseContext.Posts
                .Where(p => p.Status == PostStatus.Public)
                .OrderByDescending(p => p.Published);

            var total = await query.CountAsync();

            var result = await query
                .Include(p => p.Tags)
                .Select(p => new PostExcerptViewModel {
                    Id = p.Id,
                    Title = p.Title,
                    Excerpt = p.Excerpt,
                    Created = p.Created,
                    Modified = p.Modified,
                    Published = p.Published,
                    Tags = p.Tags
                })
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