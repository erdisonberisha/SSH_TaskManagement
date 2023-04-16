using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public TaskService(IUnitOfWork unitOfWork, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
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

        public Task<byte[]> Export(string userId, List<int> tasksId)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskEntity?> GetTaskById(int id, int userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByCondition(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
            return task;
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByCategory(int categoryId, int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.CategoryId == categoryId && x.UserId == userId && x.Status != StatusType.COMPLETED).ToListAsync();
            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetTodayTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId
                                                                                     && x.DueDate.Date == DateTime.Now.Date).Include(x => x.Comments).ThenInclude(x => x.User).ToListAsync();

            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetWeeklyTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.DueDate.Date >= DateTime.Now.Date
                                                           && x.DueDate.Date <= DateTime.Now.Date.AddDays(7)
                                                           && !x.Status.Equals(StatusType.COMPLETED)).Include(x => x.Comments).ToListAsync();
            return tasks;
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
            };
            if (task.CategoryId is null || task.CategoryId == 0)
            {
                task.CategoryId = await _categoryService.GetDefaultCategoryId(userId);
            }
            await _unitOfWork.TaskRepository.CreateAsync(task);
            await _unitOfWork.CompleteAsync();
        }

        public Task<IEnumerable<string>> SearchAutoComplete(string query, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskEntity>> SearchTasks(SearchModel search, int userId)
        {
            var userTasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.UserId == userId).ToListAsync();
            if(!string.IsNullOrEmpty(search.Query))
            {
                userTasks = userTasks.Where(x=> x.Title.Contains(search.Query) || x.Description.Contains(search.Query)).ToList();
            }
            if(search.CategoryId is not null)
            {
                userTasks = userTasks.Where(x => x.CategoryId == search.CategoryId).ToList();
            }
            if (search.Priority is not null)
            {
                userTasks = userTasks.Where(x => x.PriorityOfTask == search.Priority).ToList();

            }
            if(search.Status is not null)
            {
                userTasks = userTasks.Where(x=> x.Status == search.Status).ToList();
            }
            userTasks = userTasks.Skip((search.Page - 1) * search.PageSize).Take(search.PageSize).ToList();
            return userTasks;
        }

        public async Task<TaskEntity?> Update(int id, JsonPatchDocument<TaskEntity> task, int userId)
        {
            var taskEntity = await GetTaskById(id, userId);
            if(taskEntity is null)
            {
                throw new InvalidOperationException($"Task with id {id} and user id {userId} not found.");
            }
            task.ApplyTo(taskEntity);
            await _unitOfWork.CompleteAsync();
            return taskEntity;
        }
    }
}
