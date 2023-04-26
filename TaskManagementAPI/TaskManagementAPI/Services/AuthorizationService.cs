using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Data.UnitOfWork;
using TaskManagementAPI.Models;
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

            if (!VerifyPasswordHash(user.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
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

            CreateUserPasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

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

        private void CreateUserPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
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
