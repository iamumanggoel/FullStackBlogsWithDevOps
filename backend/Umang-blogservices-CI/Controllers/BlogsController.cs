using BlogServices.Models.Domains;
using BlogServices.Models.DTOs;
using BlogServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;
        public BlogsController(IBlogService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var result = await _service.GetAllBlogs();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPostById(Guid id)
        {
            var result = await _service.GetBlogById(id);

            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound("No such post exist");
        }


        [HttpPost]
        public async Task<IActionResult> AddBlogPost([FromBody] BlogRequest req)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            string name = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            req.UserEmail = userEmail;
            req.Author = name;
            var result = await _service.AddBlog(req);
            if(result > 0)
            {
                return Created("Blog Created successfully", req);
            }
            return BadRequest("Error in creating blog.Try Again.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogById(Guid id)
        {
            var result = await _service.DeleteBlog(id);
            if(result > 0)
            {
                return Ok("User Deleted");
            }
            return BadRequest("Error in deleting user.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBlogPost([FromBody] BlogRequest req, Guid id)
        {
            var result =  await _service.UpdateBlog(req, id);
            if(result > 0)
            {
                return Ok("User Updated");
            }
            return BadRequest("User can not be updated successfully.");
        }


    }
}
