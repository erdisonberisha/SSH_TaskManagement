using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
using TaskManagementAPI.Helpers;
using TaskManagementAPI.Models.Dto;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        public AuthorizationService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> Login(LoginDto user)
        {
            var foundUser = await _unitOfWork.UserRepository.GetByCondition(x=> x.Username == user.Username).FirstOrDefaultAsync();
            if (foundUser == null)
            {
                throw new InvalidOperationException("Invalid username.");
            }

            if (!AuthorizationHelper.VerifyPasswordHash(user.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
            {
                throw new InvalidOperationException("Invalid password.");
            }

            return GenerateJwtToken(foundUser);
        }

        public async Task Register(RegisterDto user)
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
            };

            await _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.CompleteAsync();
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
                }),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
