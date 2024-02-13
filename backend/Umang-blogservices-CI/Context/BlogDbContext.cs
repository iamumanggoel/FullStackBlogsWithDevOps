using BlogServices.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace BlogServices.Context
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<BlogPost> blogs { get; set; }
    }
}
