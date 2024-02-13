using UserServices.Models.Domains;
using UserServices.Models.DTOs;

namespace UserServices.Services
{
    public interface IUserService
    {
        Task<int> Register(User u);
        Task<bool> UserExists(User u);
        Task<int> Validate(loginRequest u);
        string CreateToken(User userInfo, IConfiguration config);
        Task<User> GetUserInfo(string email);
    }
}