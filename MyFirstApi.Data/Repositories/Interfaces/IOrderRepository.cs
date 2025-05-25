using MyFirstApi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Product?> GetProductByIdAsync(Guid productId);
        Task AddOrderProductAsync(OrderProduct orderProduct);
        Task<List<object>> GetOrdersByUserAsync(Guid userId);
        Task SaveChangesAsync();

        Task<Order?> GetOrderByIdForUserAsync(Guid orderId, Guid userId);
        Task<List<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId);
        void RemoveOrder(Order order);
        void RemoveOrderProducts(List<OrderProduct> orderProducts);

    }
}
