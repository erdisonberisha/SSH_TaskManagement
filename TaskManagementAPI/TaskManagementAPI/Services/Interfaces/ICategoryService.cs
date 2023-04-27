using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category?> GetByIdAsync(int id, int userId);
        Task<int?> GetDefaultCategoryIdAsync(int userId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync(int userId);
        Task CreateAsync(string title,int userId);
        Task<bool> DeleteAsync(int categoryId, int userId);
        Task<Category?> UpdateCategory(int id,string newTitle,int userId);
    }
}
