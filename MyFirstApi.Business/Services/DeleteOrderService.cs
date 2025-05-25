using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyFirstApi.Data;
using MyFirstApi.Services.Interfaces;

namespace MyFirstApi.Services
{
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DeleteOrderService> _logger;

        public DeleteOrderService(AppDbContext context, ILogger<DeleteOrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> DeleteOrderAsync(Guid orderId, Guid userId)
        {
            var order = _context.Orders
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
                return (false, "Order not found.");

            var orderProducts = _context.OrderProducts.Where(op => op.OrderId == orderId).ToList();
            foreach (var op in orderProducts)
            {
                var product = _context.Products.Find(op.ProductId);
                if (product != null)
                    product.IsAvailable = true;
            }

            _context.OrderProducts.RemoveRange(orderProducts);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return (true, "Order deleted and products marked as available.");
        }
    }
}
