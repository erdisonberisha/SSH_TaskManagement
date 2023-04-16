using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
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

        [HttpGet("/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.GetTaskById(id, _userId);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody]TaskCreateDto task)
        {
            await _taskService.Post(task, _userId);
            return Ok("Task created successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks= await _taskService.GetTodayTasks(_userId);
            return Ok(tasks);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            bool isDeleted = await _taskService.Delete(id, _userId);
            return !isDeleted ? NotFound() : Ok($"Task with id : {id} was deleted successfully!");
        }

        [HttpGet("/weekly")]
        public async Task<IActionResult> GetWeeklyTasks()
        {
            var tasks = await _taskService.GetWeeklyTasks(_userId);
            return Ok(tasks);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody]JsonPatchDocument<TaskEntity> taskPatch)
        {
            try
            {
                var task = await _taskService.Update(id, taskPatch, _userId);
                return Ok(task);
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
    }
}
