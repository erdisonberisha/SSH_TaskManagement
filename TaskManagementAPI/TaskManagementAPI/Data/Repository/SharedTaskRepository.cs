using TaskManagementAPI.Data.Repository.Intefaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data.Repository
{
    public class SharedTaskRepository : GenericRepository<SharedTask>, ISharedTaskRepository
    {
        public SharedTaskRepository(TaskManagementDbContext context) : base(context) { }
    }
}
