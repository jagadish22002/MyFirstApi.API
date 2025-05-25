using MyFirstApi.DTOs;
using System;
using System.Threading.Tasks;

namespace MyFirstApi.Services.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Success, string Message, object? Data)> CreateOrderAsync(Guid userId, CreateOrderDto request);
        Task<object> GetOrdersByUserAsync(Guid userId);

    }
}
