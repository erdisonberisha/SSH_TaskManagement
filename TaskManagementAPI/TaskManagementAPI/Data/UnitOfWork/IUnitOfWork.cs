using TaskManagementAPI.Data.Repository.Intefaces;

namespace TaskManagementAPI.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITaskRepository TaskRepository { get; }
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISharedTaskRepository SharedTaskRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<bool> CompleteAsync();
    }
}
