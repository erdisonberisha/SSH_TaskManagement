using TaskManagementAPI.Data.Repository.Intefaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data.Repository
{
    public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(TaskManagementDbContext context) : base(context) { }
    }
}
