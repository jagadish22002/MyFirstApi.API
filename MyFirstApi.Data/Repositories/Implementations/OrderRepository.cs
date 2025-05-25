using Microsoft.EntityFrameworkCore;
using MyFirstApi.Data;
using MyFirstApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task AddOrderProductAsync(OrderProduct orderProduct)
        {
            await _context.OrderProducts.AddAsync(orderProduct);
        }

        public async Task<List<object>> GetOrdersByUserAsync(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    o.OrderStatus,
                    Products = o.OrderProducts.Select(op => new
                    {
                        op.Product.ProductId,
                        op.Product.Name,
                        op.Product.Price
                    }).ToList()
                }).Cast<object>()
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderByIdForUserAsync(Guid orderId, Guid userId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);
        }

        public async Task<List<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderProducts.Where(op => op.OrderId == orderId).ToListAsync();
        }

        public void RemoveOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void RemoveOrderProducts(List<OrderProduct> orderProducts)
        {
            _context.OrderProducts.RemoveRange(orderProducts);
        }
    }
}
