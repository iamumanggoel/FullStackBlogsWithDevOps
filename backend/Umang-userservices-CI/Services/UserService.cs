using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserServices.Context;
using UserServices.Models.Domains;
using UserServices.Models.DTOs;

namespace UserServices.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;
        private readonly IConfiguration _config;
        public UserService(UserDbContext context)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context));
            
        }


        //Insert into table if unique
        public async Task<int> Register(User u)
        {
            if (await UserExists(u))
            {
                return 0;
            }
            else if (u is User && u is not null)
            {
                await _context.Users.AddAsync(u);
                return await _context.SaveChangesAsync();
            }
            else
                return -1;
        }


        // Select if email and password 
        public async Task<int> Validate(loginRequest u)
        {
            var found = await _context.Users.FindAsync(u.Email);
            if (found is not null)
            {
                return found.Password == u.Password ? 1 : 0;
            }
            return -1;
        }

        //find distinct
        public async Task<bool> UserExists(User u)
        {
            return await _context.Users.AnyAsync(e => e.Email == u.Email);
        }

        //generate token
        public string CreateToken(User userInfo, IConfiguration config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, $"{userInfo.FirstName} {userInfo.LastName}"),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //get user information
        public async Task<User> GetUserInfo(string email)
        {
            return await _context.Users.FindAsync(email);
           
        }

      



    }
}
