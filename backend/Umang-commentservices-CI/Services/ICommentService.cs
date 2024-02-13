using CommentServices.Models.Domains;
using CommentServices.Models.DTOs;

namespace CommentServices.Services
{
    public interface ICommentService
    {
        Task<int> AddComment(CommentRequest req, Guid BlogId);
        Task<int> DeleteComment(Guid CommentId);
        IEnumerable<Comment> GetCommentsByBlogId(Guid BlogId);
    }
}