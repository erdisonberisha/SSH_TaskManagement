using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetCurrentUserAsync(int userId);
        Task EditPasswordAsync(string password, int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
        Task CreateAdminUserAsync(RegisterDto registerDto);
        Task<User?> GetUserByIdAsync(int userId);
    }
}
