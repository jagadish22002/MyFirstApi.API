using MyFirstApi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstApi.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAvailableAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product product);
    }
}
