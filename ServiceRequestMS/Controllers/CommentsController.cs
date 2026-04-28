using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using System.Security.Claims;

namespace ServiceRequestMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateComment(CreateCommentDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(userId);
            if (Guid.TryParse(userId, out Guid userGuid))
            {
                var result = await _commentService.CreateComment(dto, userGuid);
                if (result.Success == false) return BadRequest(result);
                return Ok(result);
            }
            return Unauthorized("User ID is not valid.");
        }
        [Authorize]
        [HttpGet("{requestId}")]
        public async Task<ActionResult> GetAllComments(Guid requestId)
        {

            var result = await _commentService.GetAllComments(requestId);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
    }
}
