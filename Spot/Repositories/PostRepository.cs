
using System;
using System.Collections.Generic;
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

        internal static PostRepository Create()
        {
            throw new NotImplementedException();
        }

        public async Task<PostModel> GetWithCommentsAsync(int id)
        {
            return await DatabaseContext.Posts
                .Include(p => p.Comments)
                .SingleAsync(p => p.Id == id && p.Status == PostStatus.Public);
        }

        public async Task<PagedViewModel<PostExcerptViewModel>> GetPagedAsync(int pageIndex = 1, int pageSize = 10)
        {
            var query = DatabaseContext.Posts
                .Where(p => p.Status == PostStatus.Public)
                .OrderByDescending(p => p.Published)
                .Select(p => new PostExcerptViewModel {
                    Id = p.Id,
                    Title = p.Title,
                    Excerpt = p.Excerpt,
                    Created = p.Created,
                    Modified = p.Modified,
                    Published = p.Published
                });

            var total = await query.CountAsync();

            var result = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedViewModel<PostExcerptViewModel> {
                Pages = total > pageSize ? total / pageSize : pageSize / total,
                Index = pageIndex,
                Entities = result
            };
        }

        public async Task<IEnumerable<PostModel>> GetPagedByTagAsync(int pageIndex, int pageSize, int tagId)
        {
            return await DatabaseContext.Posts
                .Where(p => p.Status == PostStatus.Public)
                .Where(p => p.Tags.Any(t => t.Id == tagId))
                .OrderByDescending(p => p.Published)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<IEnumerable<PostModel>> GetPagedByAuthorAsync(int pageIndex, int pageSize, string author)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalCount()
        {
            return await DatabaseContext.Posts.CountAsync();
        }
    }
}