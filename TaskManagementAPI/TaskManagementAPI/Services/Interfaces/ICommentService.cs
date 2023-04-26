using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment?> EditComment(int id, JsonPatchDocument<Comment> task, int userId);
        Task WriteComment(CommentDto commentDto, int userId);
        Task<Comment?> GetCommentById(int id);
        Task<IEnumerable<Comment>> GetAllCommentsForTask(int taskId);
        Task<bool> DeleteComment(int id, int userId);
    }
}
