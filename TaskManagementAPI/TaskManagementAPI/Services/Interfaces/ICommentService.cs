using TaskManagementAPI.Models.Dto;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task EditComment(int commentId, int userId);
        Task WriteComment(CommentDto commentDto, int userId);
    }
}
