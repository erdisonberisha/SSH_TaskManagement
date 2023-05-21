using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Nest;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{

    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNotificationAsync(Notification notification, int userId)
        {
            notification.UserId = userId;
            await _unitOfWork.NotificationRepository.CreateAsync(notification);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteNotification(int id, int userId)
        {
            var notification = await GetNotificationByIdAsync(id, userId);
            if(notification == null)
            {
                return false;
            }
            _unitOfWork.NotificationRepository.Delete(notification);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<Notification?> EditNotificationAsync(int id, JsonPatchDocument<Notification> task, int userId)
        {
            var notificationEntity = await GetNotificationByIdAsync(id,userId);
            if (notificationEntity is null)
            {
                throw new InvalidOperationException($"Notification with id {id} and user id {userId} not found.");
            }
            task.ApplyTo(notificationEntity);
            await _unitOfWork.CompleteAsync();
            return notificationEntity;
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id, int userId)
        {
            return await _unitOfWork.NotificationRepository.GetByCondition(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationByUserIdAsync(int userId)
        {
            return await _unitOfWork.NotificationRepository.GetByCondition(x => x.UserId == userId).ToListAsync();

        }

        public async Task<int> GetNotificationCountAsync(int userId)
        {
            return await _unitOfWork.NotificationRepository.GetByCondition(x => x.UserId == userId && x.IsChecked == false).CountAsync();

        }
    }
}
