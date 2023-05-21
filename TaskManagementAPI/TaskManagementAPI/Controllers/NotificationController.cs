using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly int _userId;

        public NotificationController(INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            _userId = int.Parse(userIdClaim.Value);
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(Notification notification)
        {
            await _notificationService.CreateNotificationAsync(notification, _userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var deleted = await _notificationService.DeleteNotification(id, _userId);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditNotification(int id, [FromBody] JsonPatchDocument<Notification> patchDocument)
        {
            try
            {
                var updatedNotification = await _notificationService.EditNotificationAsync(id, patchDocument, _userId);
                if (updatedNotification != null)
                {
                    return Ok(updatedNotification);
                }
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id, _userId);
            return notification is null ? NotFound() : Ok(notification);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificationByUserId()
        {
            var notifications = await _notificationService.GetNotificationByUserIdAsync(_userId);
            return Ok(notifications);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetNotificationCount()
        {
            var count = await _notificationService.GetNotificationCountAsync(_userId);
            return Ok(count);
        }
    }
}
