using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task GetCurrentUserAsync(int id);
        Task EditPasswordAsync(string password, string userId);

    }
}
