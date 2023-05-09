using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly int _userId;
        private readonly IUserService _userService;
        public UserController(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            _userId = int.Parse(userIdClaim.Value);
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string password)
        {
            await _userService.EditPasswordAsync(password, _userId);
            return Ok("Password edited successfully!");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] RegisterDto user)
        {
            await _userService.CreateAdminUserAsync(user);
            return Ok("Admin created successfully!");
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userService.GetCurrentUserAsync(_userId);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchUsernames(string username)
        {
            var users = await _userService.SearchUsernamesAsync(username);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        { 
            bool isDeleted = await _userService.DeleteUserAsync(id);
            return !isDeleted ? NotFound() : Ok($"User with id : {id} was deleted successfully!");
        }
    }
}
