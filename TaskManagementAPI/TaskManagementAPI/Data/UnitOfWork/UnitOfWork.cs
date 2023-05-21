using TaskManagementAPI.Data.Repository;
using TaskManagementAPI.Data.Repository.Intefaces;

namespace TaskManagementAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories;
        private readonly TaskManagementDbContext _dbContext;

        public UnitOfWork(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<string, object>();
        }

        public ITaskRepository TaskRepository => GetRepository<ITaskRepository, TaskRepository>();
        public ICategoryRepository CategoryRepository => GetRepository<ICategoryRepository, CategoryRepository>();
        public ISharedTaskRepository SharedTaskRepository => GetRepository<ISharedTaskRepository, SharedTaskRepository>();
        public ICommentRepository CommentRepository => GetRepository<ICommentRepository, CommentRepository>();
        public IUserRepository UserRepository => GetRepository<IUserRepository, UserRepository>();
        public INotificationRepository NotificationRepository => GetRepository<INotificationRepository, NotificationRepository>();
        // Add other repository properties similarly

        private TInterface GetRepository<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            string typeName = typeof(TInterface).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                object repositoryInstance = Activator.CreateInstance(typeof(TImplementation), _dbContext);
                _repositories[typeName] = repositoryInstance;
            }

            return (TInterface)_repositories[typeName];
        }

        public async Task<bool> CompleteAsync()
        {
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }
        public void Dispose() => _dbContext.Dispose();
    }

}
