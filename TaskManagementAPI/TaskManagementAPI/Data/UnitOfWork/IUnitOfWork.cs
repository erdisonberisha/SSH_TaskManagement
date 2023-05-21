using TaskManagementAPI.Data.Repository.Intefaces;

namespace TaskManagementAPI.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskRepository { get; }
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISharedTaskRepository SharedTaskRepository { get; }
        ICommentRepository CommentRepository { get; }
        INotificationRepository NotificationRepository { get; }
        Task<bool> CompleteAsync();
    }
}
