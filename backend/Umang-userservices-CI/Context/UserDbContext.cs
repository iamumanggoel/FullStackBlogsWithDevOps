using Microsoft.EntityFrameworkCore;
using UserServices.Models.Domains;

namespace UserServices.Context
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
