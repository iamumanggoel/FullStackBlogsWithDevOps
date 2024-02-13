using CommentServices.Models.DTOs;
using CommentServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace CommentServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentsController(ICommentService service) {
            _service = service;
        }

        [HttpGet("{BlogId}")]
        public async Task<IActionResult> GetComments(Guid BlogId)
        {
            var result = _service.GetCommentsByBlogId(BlogId);

            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("{blogId}")]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest req, Guid blogId)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            string name = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            req.UserEmail = userEmail;
            req.Name = name;
            var result = await _service.AddComment(req, blogId);

            if(result > 0)
            {
                return Ok(req);
            }
            return BadRequest("Comment can not be added.");
        }
    }
}
