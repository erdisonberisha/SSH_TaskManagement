using TaskManagementAPI.Data.Repository.Intefaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TaskManagementDbContext context) : base(context) { }
    }
}
