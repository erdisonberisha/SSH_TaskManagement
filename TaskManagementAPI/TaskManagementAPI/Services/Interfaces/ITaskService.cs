using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetTodayTasks(string userId);
        Task<IEnumerable<TaskEntity>> GetWeeklyTasks(string userId);
        Task<TaskEntity> GetTaskById(int id, string userId);
        Task<IEnumerable<TaskEntity>> GetTaskByProject(string projectId);
        Task<TaskEntity> Filter(TaskEntity filter, string userId);
        Task<IEnumerable<TaskEntity>> SearchTasksByKeywords(string keywords, int pageNumer, int pageSize, string userId);
        Task<IEnumerable<string>> SearchAutoComplete(string keywords, string userId);
        Task Post(TaskEntity taskToCreate, string userId);
        Task Update(int id, JsonPatchDocument<TaskEntity> task, string userId);
        Task Delete(int id, string userId);
        Task<byte[]> Export(string userId, List<int> tasksId);
        Task Import(IFormFile formFile);
    }
}
