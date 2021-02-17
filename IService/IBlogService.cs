using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<PostModel>> Get();
        Task<string> Create(Post post);
        Task<string> Modify(Post post);
        Task<string> Delete(int postId);
    }
}
