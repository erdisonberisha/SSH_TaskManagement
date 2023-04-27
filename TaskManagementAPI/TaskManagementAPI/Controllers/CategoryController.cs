using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;


namespace TaskManagementAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly int _userId;
        public CategoryController(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            _userId = int.Parse(userIdClaim.Value);
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync(_userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _categoryService.GetByIdAsync(id, _userId);
            return comment is null ? NotFound() : Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] string title)
        {
            await _categoryService.CreateAsync(title, _userId);
            return Ok("Task created successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,[FromBody] string title)
        {
            var category = await _categoryService.UpdateCategory(id,title, _userId);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _categoryService.DeleteAsync(id, _userId);
            return !isDeleted ? NotFound() : Ok($"Category with id : {id} was deleted successfully!");
        }
    }
}
