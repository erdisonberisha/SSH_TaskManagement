﻿using CsvHelper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly INotificationService _notificationService;

        public TaskService(IUnitOfWork unitOfWork, ICategoryService categoryService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _notificationService = notificationService;
        }

        public async Task ApproveInvite(int userId, int taskId)
        {
            var sharedTask = await _unitOfWork.SharedTaskRepository.GetByCondition(x => x.UserId == userId && x.TaskId == taskId && x.Approved == false).FirstOrDefaultAsync();
            if (sharedTask != null)
            {
                sharedTask.Approved = true;
                _unitOfWork.SharedTaskRepository.Update(sharedTask);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                throw new InvalidOperationException("Invite for task was not found");
            }
        }

        public async Task<IEnumerable<SharedTask>> GetPendingInvitesAsync(int userId)
        {
            return await _unitOfWork.SharedTaskRepository.GetByCondition(x=> x.UserId == userId && x.Approved == false).Include(x=> x.Task).ToListAsync();
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var task = await GetTaskById(id, userId);
            if (task != null)
            {
                _unitOfWork.TaskRepository.Delete(task);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<byte[]> ExportAsync(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId).ToListAsync();
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            using (var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture))
            {
                await csvWriter.WriteRecordsAsync(tasks);
                await streamWriter.FlushAsync();
                return memoryStream.ToArray();
            }
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId).ToListAsync();
            return tasks;
        }

        public async Task<TaskEntity?> GetTaskById(int id, int userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByCondition(x => x.Id == id && x.UserId == userId).Include(x => x.Comments).FirstOrDefaultAsync();
            return task;
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByCategory(int categoryId, int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetTodayTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId
                                                                                     && x.DueDate.Date == DateTime.Now.Date).Include(x => x.Comments).ThenInclude(x => x.User).ToListAsync();
            return tasks;
        }
        
        public async Task<IEnumerable<TaskEntity>> GetSharedTasks(int userId)
        {
            var tasks = await _unitOfWork.SharedTaskRepository.GetByCondition(x => x.UserId==userId).Include(x=> x.Task).Select(x => x.Task).ToListAsync();
            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetWeeklyTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.DueDate.Date >= DateTime.Now.Date
                                                           && x.DueDate.Date <= DateTime.Now.Date.AddDays(7)).Include(x => x.Comments).ToListAsync();
            return tasks;
        }

        public async Task InviteUserToTask(int taskId, string username, int userId)
        {
            var task = (await _unitOfWork.TaskRepository.GetById(x => x.Id == taskId && x.UserId == userId).Include(x=>x.User)
                                                             .FirstOrDefaultAsync());
            if (task is not null)
            {
                var userToInvite = (await _unitOfWork.UserRepository.GetByCondition(x => x.Username == username).FirstOrDefaultAsync()).Id;
                var sharedTask = new SharedTask
                {
                    UserId = userToInvite,
                    TaskId = taskId
                };
                await _unitOfWork.SharedTaskRepository.CreateAsync(sharedTask);
                await _unitOfWork.CompleteAsync();
                var notification = new Notification
                {
                    Title = "You have been invited to task " + task.Title + " by user " + task.User.Name + " with username " + task.User.Username,
                    Description = task.Description,
                };
                await _notificationService.CreateNotificationAsync(notification, userId);
            }
            else
            {
                throw new InvalidOperationException("Invite is not possible");
            }
        }

        public async Task Post(TaskCreateDto taskToCreate, int userId)
        {
            var task = new TaskEntity
            {
                Title = taskToCreate.Title,
                UserId = userId,
                PriorityOfTask = taskToCreate.PriorityOfTask,
                CategoryId = taskToCreate.CategoryId,
                Description = taskToCreate.Description,
                DueDate = taskToCreate.DueDate,
                Status = taskToCreate.Status ?? StatusType.TODO
            };
            if (task.CategoryId is null || task.CategoryId == 0)
            {
                task.CategoryId = await _categoryService.GetDefaultCategoryIdAsync(userId);
            }
            await _unitOfWork.TaskRepository.CreateAsync(task);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<string>> SearchAutoComplete(string query, int userId)
        {
            List<string> autocomplete = new();
            var userTasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId).OrderByDescending(x => x.DueDate).ToListAsync();
            if (!string.IsNullOrEmpty(query))
                autocomplete = userTasks.Where(x => x.Title.Contains(query) || x.Description.Contains(query)).Take(5).Select(x => x.Title).Distinct().ToList();
            return autocomplete;
        }

        public async Task<IEnumerable<TaskEntity>> SearchTasks(SearchModel search, int userId)
        {
            var userTasks = _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId);
            if (!string.IsNullOrEmpty(search.Query))
            {
                userTasks = userTasks.Where(x => x.Title.Contains(search.Query) || x.Description.Contains(search.Query));
            }
            if (search.CategoryId is not null)
            {
                userTasks = userTasks.Where(x => x.CategoryId == search.CategoryId);
            }
            if (search.Priority is not null)
            {
                userTasks = userTasks.Where(x => x.PriorityOfTask == search.Priority);

            }
            if (search.Status is not null)
            {
                userTasks = userTasks.Where(x => x.Status == search.Status);
            }
            userTasks = userTasks.Skip((search.Page - 1) * search.PageSize).Take(search.PageSize);
            return await userTasks.ToListAsync();
        }

        public async Task<TaskEntity?> Update(int id, JsonPatchDocument<TaskEntity> task, int userId)
        {
            var taskEntity = await GetTaskById(id, userId);
            if (taskEntity is null)
            {
                throw new InvalidOperationException($"Task with id {id} and user id {userId} not found.");
            }
            task.ApplyTo(taskEntity);
            await _unitOfWork.CompleteAsync();
            return taskEntity;
        }
    }
}
