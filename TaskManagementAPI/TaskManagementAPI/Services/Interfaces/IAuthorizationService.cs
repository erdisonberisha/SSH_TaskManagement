using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Models.Dto;

namespace TaskManagementAPI.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task Register(RegisterDto user);
        Task<string> Login(LoginDto user);
    }
}
