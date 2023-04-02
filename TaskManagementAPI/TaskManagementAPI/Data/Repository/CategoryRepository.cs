using TaskManagementAPI.Data.Repository.Intefaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TaskManagementDbContext context) : base(context) { }
    }
}
