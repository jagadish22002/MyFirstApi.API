using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Data;
using MyFirstApi.Services.Interfaces;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null.");

            var result = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = result.ProductId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await _service.GetAllAsync());

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableProducts() => Ok(await _service.GetAvailableAsync());
    }
}
