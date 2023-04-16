using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<int?> GetDefaultCategoryId(int userId);
        Task CreateAsync(Category category,string userId);
        Task DeleteAsync(int categoryId, string userId);
        Task UpdateCategory(Category category,string userId);
    }
}
