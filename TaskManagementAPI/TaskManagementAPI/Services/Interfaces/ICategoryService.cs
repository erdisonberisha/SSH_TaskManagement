using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<int?> GetDefaultCategoryId(int userId);
        Task CreateAsync(Category category,int userId);
        Task DeleteAsync(int categoryId, int userId);
        Task UpdateCategory(Category category,int userId);
    }
}
