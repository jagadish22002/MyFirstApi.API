using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Services.Interfaces;
using System.Threading.Tasks;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly IUserLoginLogoutService _userService;

        public LogoutController(IUserLoginLogoutService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var message = await _userService.LogoutAsync();
            return Ok(message);
        }
    }
}
