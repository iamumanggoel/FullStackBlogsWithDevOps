using CommentServices.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CommentServices.Context
{
    public class CommentDbContext:DbContext
    {
        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }
        public DbSet<Comment> Comments { get; set; }
    }
}
