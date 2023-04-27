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

        public async Task CreateAsync(string title, int userId)
        {
            var category = new Category
            {
                Title = title,
                UserId = userId
            };
            await _unitOfWork.CategoryRepository.CreateAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteAsync(int categoryId, int userId)
        {
            var category = await GetByIdAsync(categoryId, userId);
            if(category != null)
            {
                _unitOfWork.CategoryRepository.Delete(category);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int userId)
        {
            var categories = await _unitOfWork.CategoryRepository.GetByCondition(x=> x.UserId == userId).ToListAsync();
            return categories;
        }

        public async Task<Category?> GetByIdAsync(int id, int userId)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(x=> x.Id == id && x.UserId == userId).FirstOrDefaultAsync();
            if (category is null)
            {
                throw new InvalidOperationException($"Category with id {id} and user id {userId} was not found.");
            }
            return category;
        }

        public async Task<int?> GetDefaultCategoryIdAsync(int userId)
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

        public async Task<Category?> UpdateCategory(int id, string newTitle, int userId)
        {
            var category = await GetByIdAsync(id, userId);
            if (category is null)
            {
                throw new InvalidOperationException($"Category with id {id} and user id {userId} not found.");
            }
            category.Title = newTitle;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.CompleteAsync();
            return category;
        }
    }
}
