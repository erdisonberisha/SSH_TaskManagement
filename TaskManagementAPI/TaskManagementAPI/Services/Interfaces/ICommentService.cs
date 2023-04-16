using Microsoft.AspNetCore.JsonPatch;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task EditComment(int id, JsonPatchDocument<Comment> task, int userId);
        Task WriteComment(CommentDto commentDto, int userId);
    }
}
