using Microsoft.AspNetCore.Mvc;
using MyFirstApi.DTOs;
using MyFirstApi.Services.Interfaces;
using System.Threading.Tasks;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserLoginLogoutService _userService;

        public LoginController(IUserLoginLogoutService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _userService.LoginAsync(request);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
