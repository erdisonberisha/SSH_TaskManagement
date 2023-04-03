using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetTodayTasks(int userId);
        Task<IEnumerable<TaskEntity>> GetWeeklyTasks(int userId);
        Task<TaskEntity?> GetTaskById(int id, int userId);
        Task<IEnumerable<TaskEntity>> GetTasksByCategory(int categoryId, int userId);
        Task<TaskEntity> Filter(TaskEntity filter, int userId);
        Task<IEnumerable<TaskEntity>> SearchTasksByKeywords(string keywords, int pageNumer, int pageSize, int userId);
        Task<IEnumerable<string>> SearchAutoComplete(string keywords, int userId);
        Task Post(TaskEntity taskToCreate, int userId);
        Task Update(int id, JsonPatchDocument<TaskEntity> task, int userId);
        Task<bool> Delete(int id, int userId);
        Task<byte[]> Export(string userId, List<int> tasksId);
        Task Import(IFormFile formFile);
    }
}
