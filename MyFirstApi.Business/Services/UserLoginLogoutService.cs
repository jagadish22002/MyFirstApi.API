using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFirstApi.Data.Repositories;
using MyFirstApi.DTOs;
using MyFirstApi.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstApi.Services
{
    public class UserLoginLogoutService : IUserLoginLogoutService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserLoginLogoutService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<(bool Success, string? Message, object? Data)> LoginAsync(LoginDto request)
        {
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, request.Password);
            if (user == null)
            {
                return (false, "Invalid credentials", null);
            }

            var claims = new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var keyString = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not found in configuration.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return (true, null, new
            {
                token = jwt,
                userId = user.UserId,
                name = user.Name,
                email = user.Email
            });
        }

        public Task<string> LogoutAsync()
        {
            // In real apps, implement token blacklisting here
            return Task.FromResult("You have been logged out.");
        }
    }
}
