using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Notification?> EditNotificationAsync(int id, JsonPatchDocument<Notification> task, int userId);
        Task CreateNotificationAsync(Notification commentDto, int userId);
        Task<IEnumerable<Notification>> GetNotificationByUserIdAsync(int userId);
        Task<Notification?> GetNotificationByIdAsync(int id, int userId);
        Task<bool> DeleteNotification(int id, int userId);
        Task<int> GetNotificationCountAsync(int userId);
    }
}
