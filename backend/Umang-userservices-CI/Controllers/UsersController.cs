using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using UserServices.Models.Domains;
using UserServices.Models.DTOs;
using UserServices.Services;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration _config;
        public UsersController(IUserService service, IConfiguration config) {
            
            _service = service;
            _config = config;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest user)
        {
            try
            {
                User newUser = new User
                {
                    Email = user.Email,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    DOB = user.DOB,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                };
                var result = await _service.Register(newUser);

                if(result > 0 )
                {
                    return Created();
                }
                else if(result == 0 )
                {
                    return Conflict();
                }
                else 
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginRequest loginUser)
        {
            try
            {

                var result = await _service.Validate(loginUser);
                if(result > 0)
                {
                    var user = await _service.GetUserInfo(loginUser.Email);
                    var tokenString = _service.CreateToken(user, _config);
                    return Ok(new { token = tokenString });
                }
                else if(result == 0)
                {
                    return NotFound("Wrong Password. Please Try Again.");
                }
                else
                {
                    return NotFound("No such User Exist.");
                }
            }catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [Authorize]
        [HttpGet("/getinfo")]
        public async Task<IActionResult> GetInfo()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _service.GetUserInfo(userEmail);

            if (user is User && user is not null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }

    }
}
