using MyFirstApi.Data;
using MyFirstApi.DTOs;
using MyFirstApi.Repositories.Interfaces;
using MyFirstApi.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool Success, string Message, object? Data)> CreateOrderAsync(Guid userId, CreateOrderDto request)
        {
            if (request.ProductIds == null || !request.ProductIds.Any())
                return (false, "At least one product ID is required.", null);

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "OnWay"
            };

            await _repository.AddOrderAsync(order);

            foreach (var productId in request.ProductIds)
            {
                var product = await _repository.GetProductByIdAsync(productId);
                if (product == null || !product.IsAvailable)
                    return (false, $"Product {productId} is not available.", null);

                product.IsAvailable = false;

                await _repository.AddOrderProductAsync(new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = productId
                });
            }

            await _repository.SaveChangesAsync();
            return (true, "Order created successfully", new { order.OrderId });
        }

        public async Task<object> GetOrdersByUserAsync(Guid userId)
        {
            return await _repository.GetOrdersByUserAsync(userId);
        }
    }
}
