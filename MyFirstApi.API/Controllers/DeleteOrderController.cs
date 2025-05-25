using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Services.Interfaces; // <-- make sure this namespace is correct
using System.Security.Claims;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/delete-order")]
    public class DeleteOrderController : ControllerBase
    {
        private readonly IDeleteOrderService _deleteOrderService; // <- use the interface

        public DeleteOrderController(IDeleteOrderService deleteOrderService) // <- inject the interface
        {
            _deleteOrderService = deleteOrderService;
        }

        [HttpDelete("{orderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "userId" ||
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "sub");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized("Invalid or missing user ID.");

            var (success, message) = await _deleteOrderService.DeleteOrderAsync(orderId, userId);
            return success ? Ok(message) : NotFound(message);
        }
    }
}
