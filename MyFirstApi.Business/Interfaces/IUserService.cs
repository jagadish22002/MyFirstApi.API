// IUserService.cs
using MyFirstApi.Data;
using System;
using System.Threading.Tasks;

namespace MyFirstApi.Business.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id, string? requesterUserId);
        Task<(bool IsSuccess, string? ErrorMessage, User? User)> CreateUserAsync(User user);
    }
}
