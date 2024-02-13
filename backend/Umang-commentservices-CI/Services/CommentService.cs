using CommentServices.Context;
using CommentServices.Models.Domains;
using CommentServices.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CommentServices.Services
{
    public class CommentService : ICommentService
    {
        private readonly CommentDbContext _context;
        public CommentService(CommentDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();

        }

        public IEnumerable<Comment> GetCommentsByBlogId(Guid BlogId)
        {
            var find = _context.Comments.Where(id => id.BlogId == BlogId);
            return find;
        }

        public async Task<int> AddComment(CommentRequest req, Guid BlogId)
        {
            if (req is CommentRequest && req is not null)
            {
                Comment newComment = new Comment()
                {
                    BlogId = BlogId,
                    CommentText = req.CommentText,
                    UserEmail = req.UserEmail,
                    Name = req.Name,
                };

                await _context.Comments.AddAsync(newComment);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<int> DeleteComment(Guid CommentId)
        {
            var find = await _context.Comments.FindAsync(CommentId);
            if (find is not null)
            {
                _context.Remove(find);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }
    }
}
