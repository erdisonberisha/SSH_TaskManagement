using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Category category, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int categoryId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> GetDefaultCategoryId(int userId)
        {
            var category = await _unitOfWork.CategoryRepository.GetByCondition(x=> x.UserId== userId && x.Title == "Inbox").FirstOrDefaultAsync();
            if(category is not null)
            {
                return category.Id;
            }
            var createCategory = new Category
            {
                Title = "Inbox",
                UserId = userId,
            };
            await _unitOfWork.CategoryRepository.CreateAsync(createCategory);
            await _unitOfWork.CompleteAsync();
            return createCategory.Id;
        }

        public Task UpdateCategory(Category category, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
