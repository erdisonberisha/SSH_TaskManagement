using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Controllers
{
    [Route("api/tasks")]
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

        [HttpGet("{id}")]
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

        [HttpGet("daily")]
        public async Task<IActionResult> GetTodayTasks()
        {
            return Ok(await _taskService.GetTodayTasks(_userId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            bool isDeleted = await _taskService.Delete(id, _userId);
            return !isDeleted ? NotFound() : Ok($"Task with id : {id} was deleted successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasks(_userId);
            return Ok(tasks);
        }

        [HttpGet("invites")]
        public async Task<IActionResult> GetAllInvites()
        {
            var tasks = await _taskService.GetPendingInvitesAsync(_userId);
            return Ok(tasks);
        }

        [HttpGet("weekly")]
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

        [HttpPost("invites")]
        public async Task<IActionResult> InviteUserToTask(int taskId, [FromBody]string username)
        {
            try
            {
                await _taskService.InviteUserToTask(taskId, username, _userId);
                return Ok("User invited successfully");
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

        [HttpPut("invites/approve/{taskId}")]
        public async Task<IActionResult> ApproveInvite(int taskId)
        {
            try
            {
                await _taskService.ApproveInvite(_userId,taskId);
                return Ok("Invite was approved");
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
        [HttpGet("search")]
        public async Task<IActionResult> SeachForTasks([FromQuery]SearchModel searchModel)
        {
            var tasks = await _taskService.SearchTasks(searchModel, _userId);
            return Ok(tasks);
        }

        [HttpGet("autocomplete")]
        public async Task<IActionResult> AutocompleteFromQuery(string query)
        {
            var taskTitles = await _taskService.SearchAutoComplete(query, _userId);
            return Ok(taskTitles);
        }

        [HttpGet("export-to-csv")]
        public async Task<IActionResult> ExportTasksToCsvAsync()
        {
            var csvBytes = await _taskService.ExportAsync(_userId);
            return File(csvBytes, "text/csv", "tasks.csv");
        }
    }
}
