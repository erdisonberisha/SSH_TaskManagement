using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;


namespace TaskManagementAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly int _userId;

        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            _userId = int.Parse(userIdClaim.Value);
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var task = await _commentService.GetCommentById(commentId);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int taskId)
        {
            var commentsByTaskId = await _commentService.GetAllCommentsForTask(taskId);
            return Ok(commentsByTaskId);
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Post(CommentDto commentDto)
        {
            await _commentService.WriteComment(commentDto, _userId);
            return Ok("Comment was added to task");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] JsonPatchDocument<Comment> commentPatch)
        {
            try
            {
                var comment = await _commentService.EditComment(id, commentPatch, _userId);
                return Ok(comment);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _commentService.DeleteComment(id, _userId);
            return !isDeleted ? NotFound() : Ok($"Comment with id : {id} was deleted successfully!");
        }
    }
}
