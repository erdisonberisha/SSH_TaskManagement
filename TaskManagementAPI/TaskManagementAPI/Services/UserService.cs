using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Helpers;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task CreateAdminUserAsync(RegisterDto user)
        {
            if (await _unitOfWork.UserRepository.GetByCondition(x => x.Username == user.Username).FirstOrDefaultAsync() != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            AuthorizationHelper.CreateUserPasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DateOfBirth = user.DateOfBirth,
                Role = "Admin"
            };

            await _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.CompleteAsync();
        }
    
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if(user != null)
            {
                if (user.Role == "Admin")
                    throw new InvalidOperationException("You cannot delete another admin!");
                _unitOfWork.UserRepository.Delete(user);
            }
            return await _unitOfWork.CompleteAsync();
        }

        public async Task EditPasswordAsync(string password, int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                AuthorizationHelper.CreateUserPasswordHash("password", out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash; user.PasswordSalt = passwordSalt;
                _unitOfWork.UserRepository.Update(user);
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAll().ToListAsync();
        }

        public async Task<User?> GetCurrentUserAsync(int userId)
        {
            return await GetUserByIdAsync(userId);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _unitOfWork.UserRepository.GetById(x => x.Id == userId).FirstOrDefaultAsync();
        }
    }
}
