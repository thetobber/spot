
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Spot.Models;
using Spot.Models.Post;
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

        public async Task<IEnumerable<PostModel>> GetPagedAsync(int pageIndex, int pageSize)
        {
            return await DatabaseContext.Posts
                .Where(p => p.Status == PostStatus.Public)
                .OrderByDescending(p => p.Published)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
    }
}