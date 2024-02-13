using BlogServices.Context;
using BlogServices.Models.Domains;
using BlogServices.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlogServices.Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;
        public BlogService(BlogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogs()
        {
            return await _context.blogs.ToListAsync();
        }

        public async Task<BlogPost> GetBlogById(Guid id)
        {
            return await _context.blogs.FindAsync(id);
            
        }

        public async Task<int> AddBlog(BlogRequest blog)
        {
            if (blog is BlogRequest && blog is not null)
            {
                BlogPost b = new BlogPost
                {
                    Author = blog.Author,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    PublishedDate = blog.PublishedDate,
                    ShortDescription = blog.ShortDescription,
                    UserEmail = blog.UserEmail,
                    Title = blog.Title,
                };

                await _context.AddAsync(b);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<int> DeleteBlog(Guid id)
        {
            var find = await _context.blogs.FindAsync(id);
            if (find != null)
            {
                _context.Remove(find);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<int> UpdateBlog(BlogRequest blog, Guid id)
        {
            var find = await _context.blogs.FindAsync(id);

            if (find is BlogPost && find is not null)
            {
                find.Author = blog.Author;
                find.Content = blog.Content;
                find.ShortDescription = blog.ShortDescription;
                find.PublishedDate = blog.PublishedDate;
                find.Title = blog.Title;
                find.FeaturedImageUrl = blog.FeaturedImageUrl;

                return await _context.SaveChangesAsync();
            }
            return -1;
        }
    }
}
