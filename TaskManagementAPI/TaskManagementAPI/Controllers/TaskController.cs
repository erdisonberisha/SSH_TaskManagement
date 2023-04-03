using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly int _userId;
        private readonly ITaskService _taskService;
        public TaskController(IHttpContextAccessor httpContextAccessor, ITaskService taskService)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            _userId = int.Parse(userIdClaim.Value);
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) 
        {
            var task = await _taskService.GetTaskById(id, _userId);
            return task is null ? NotFound() : Ok(task);
        }
    }
}
