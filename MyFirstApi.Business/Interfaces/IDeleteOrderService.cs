using System;
using System.Threading.Tasks;

namespace MyFirstApi.Services.Interfaces
{
    public interface IDeleteOrderService
    {
        Task<(bool Success, string Message)> DeleteOrderAsync(Guid orderId, Guid userId);
    }
}
