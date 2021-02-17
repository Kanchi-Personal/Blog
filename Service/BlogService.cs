using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _dbContext;
        public BlogService(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<PostModel>> Get()
        {
            var posts = (from post in _dbContext.Posts
                         select new PostModel
                         {
                             Title = post.Title,
                             Summary = post.Summary,
                             UserName = _dbContext.Users.FirstOrDefault(x => x.UserId == post.UserId) == null ? "Unknown" 
                             : _dbContext.Users.FirstOrDefault(x => x.UserId == post.UserId).UserFullName
                         });
            return await posts.ToListAsync();
        }
        public async Task<string> Create(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return "Created Successfully.";
        }
        public async Task<string> Modify(Post post)
        {
            var postInDb = await _dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == post.PostId);
            if (postInDb == null) return "Invalid request.";
            postInDb.Title = post.Title;
            postInDb.Summary = post.Summary;
            await _dbContext.SaveChangesAsync();
            return "Modified Successfully.";
        }
        public async Task<string> Delete(int postId)
        {
            var postInDb = await _dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
            if (postInDb == null) return "Invalid request.";
            _dbContext.Posts.Remove(postInDb);
            await _dbContext.SaveChangesAsync();
            return "Deleted Successfully.";
        }

    }
}
