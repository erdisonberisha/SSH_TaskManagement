using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByCondition(x=> x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
            if(task != null)
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

        public Task<TaskEntity> Filter(TaskEntity filter, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskEntity?> GetTaskById(int id, int userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByCondition(x=> x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
            return task;
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByCategory(int categoryId, int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x => x.CategoryId == categoryId && x.UserId == userId && x.Status != StatusType.COMPLETED).ToListAsync();
            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetTodayTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByCondition(x=> x.UserId== userId
                                                                                     && x.DueDate.Date == DateTime.Now.Date).ToListAsync();

            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetWeeklyTasks(int userId)
        {
            var tasks = await _unitOfWork.TaskRepository
                                      .GetByCondition(x=> x.DueDate.Date >= DateTime.Now.Date
                                                           && x.DueDate.Date <= DateTime.Now.Date.AddDays(7)
                                                           &&!x.Status.Equals(StatusType.COMPLETED)).ToListAsync();
            return tasks;
        }

        public Task Import(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public async Task Post(TaskEntity taskToCreate, int userId)
        {
            taskToCreate.UserId= userId;
            await _unitOfWork.TaskRepository.CreateAsync(taskToCreate);
            await _unitOfWork.CompleteAsync();
        }

        public Task<IEnumerable<string>> SearchAutoComplete(string keywords, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskEntity>> SearchTasksByKeywords(string keywords, int pageNumer, int pageSize, int userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, JsonPatchDocument<TaskEntity> task, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
