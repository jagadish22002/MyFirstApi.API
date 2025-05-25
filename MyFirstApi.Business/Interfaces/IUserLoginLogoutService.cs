using MyFirstApi.DTOs;
using System.Threading.Tasks;

namespace MyFirstApi.Services.Interfaces
{
    public interface IUserLoginLogoutService
    {
        Task<(bool Success, string? Message, object? Data)> LoginAsync(LoginDto request);
        Task<string> LogoutAsync();
    }
}
