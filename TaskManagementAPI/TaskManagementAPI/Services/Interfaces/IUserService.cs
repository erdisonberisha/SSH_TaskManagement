using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetCurrentUserAsync(int id);
        Task EditPasswordAsync(string password, string userId);


    }
}
