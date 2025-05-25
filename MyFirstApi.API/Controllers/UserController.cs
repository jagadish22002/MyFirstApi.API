using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Business.Services;
using MyFirstApi.Data;
using System;
using System.Threading.Tasks;

namespace MyFirstApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var userIdFromToken = User.FindFirst("userId")?.Value;

            var user = await _userService.GetUserByIdAsync(id, userIdFromToken);

            if (user == null)
            {
                return Forbid("You are not authorized to access this user's data or user not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(user);

            if (!result.IsSuccess)
                return Conflict(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(GetUserById), new { id = result.User!.UserId }, result.User);
        }
    }
}
