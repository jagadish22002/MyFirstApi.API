using MyFirstApi.Data;
using MyFirstApi.Data.Repositories;
using MyFirstApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();

        public Task<IEnumerable<Product>> GetAvailableAsync() => _repository.GetAvailableAsync();

        public Task<Product?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task<Product> CreateAsync(Product product) => _repository.CreateAsync(product);
    }
}
