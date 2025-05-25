// IUserRepository.cs
using MyFirstApi.Data;
using System;
using System.Threading.Tasks;

namespace MyFirstApi.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);

    }
}
