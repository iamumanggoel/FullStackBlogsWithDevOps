using BlogServices.Models.Domains;
using BlogServices.Models.DTOs;

namespace BlogServices.Services
{
    public interface IBlogService
    {
        Task<int> AddBlog(BlogRequest blog);
        Task<int> DeleteBlog(Guid id);
        Task<IEnumerable<BlogPost>> GetAllBlogs();
        Task<BlogPost> GetBlogById(Guid id);
        Task<int> UpdateBlog(BlogRequest blog, Guid id);
    }
}