namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task EditComment(int commentId, int userId);
    }
}
