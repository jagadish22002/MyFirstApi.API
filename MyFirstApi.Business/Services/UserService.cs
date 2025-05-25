// UserService.cs
using MyFirstApi.Data;
using MyFirstApi.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace MyFirstApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(Guid id, string? requesterUserId)
        {
            if (string.IsNullOrWhiteSpace(requesterUserId) || requesterUserId != id.ToString())
            {
                // Authorization failed, return null or throw exception as needed
                return null;
            }

            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, User? User)> CreateUserAsync(User user)
        {
            bool emailExists = await _userRepository.EmailExistsAsync(user.Email);
            if (emailExists)
            {
                return (false, "A user with this email already exists.", null);
            }

            user.UserId = Guid.NewGuid();

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, null, user);
        }
    }
}
