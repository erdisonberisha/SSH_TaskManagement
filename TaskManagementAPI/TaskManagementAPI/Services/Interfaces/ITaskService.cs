﻿using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetTodayTasks(int userId);
        Task<IEnumerable<TaskEntity>> GetWeeklyTasks(int userId);
        Task<TaskEntity?> GetTaskById(int id, int userId);
        Task<IEnumerable<TaskEntity>> GetTasksByCategory(int categoryId, int userId);
        Task<IEnumerable<TaskEntity>> SearchTasks(SearchModel search, int userId);
        Task<IEnumerable<string>> SearchAutoComplete(string query, int userId);
        Task Post(TaskCreateDto taskToCreate, int userId);
        Task<TaskEntity?> Update(int id, JsonPatchDocument<TaskEntity> task, int userId);
        Task<bool> Delete(int id, int userId);
        Task<byte[]> Export(string userId, List<int> tasksId);
    }
}