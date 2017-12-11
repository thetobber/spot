
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Spot;
using Spot.Models;
using Spot.Repositories.Generic;

namespace Spot.Repositories
{
    public class PostRepository : Repository<int, Post>, IPostRepository
    {
        public AppDbContext Spot => Context as AppDbContext;

        public PostRepository(AppDbContext context) : base(context)
        {
        }

        internal static PostRepository Create()
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetWithCommentsAsync(int id)
        {
            return await Spot.Posts
                .Include(p => p.Comments)
                .SingleAsync(p => p.Id == id && p.Status == PostStatus.Public);
        }

        public async Task<IEnumerable<Post>> GetPagedAsync(int pageIndex, int pageSize)
        {
            return await Spot.Posts
                .Where(p => p.Status == PostStatus.Public)
                .OrderByDescending(p => p.Published)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPagedByTagAsync(int pageIndex, int pageSize, int tagId)
        {
            return await Spot.Posts
                .Where(p => p.Status == PostStatus.Public)
                .Where(p => p.Tags.Any(t => t.Id == tagId))
                .OrderByDescending(p => p.Published)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<IEnumerable<Post>> GetPagedByAuthorAsync(int pageIndex, int pageSize, string author)
        {
            throw new NotImplementedException();
        }
    }
}