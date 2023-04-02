using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        public Task Delete(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> Export(string userId, List<int> tasksId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskEntity> Filter(TaskEntity filter, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskEntity> GetTaskById(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskEntity>> GetTaskByProject(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskEntity>> GetTodayTasks(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskEntity>> GetWeeklyTasks(string userId)
        {
            throw new NotImplementedException();
        }

        public Task Import(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public Task Post(TaskEntity taskToCreate, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> SearchAutoComplete(string keywords, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskEntity>> SearchTasksByKeywords(string keywords, int pageNumer, int pageSize, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, JsonPatchDocument<TaskEntity> task, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
