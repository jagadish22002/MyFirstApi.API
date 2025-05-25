using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstApi.Data
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        // Optional: Redundant product name for easy access (not normalized)
        public string ProductName { get; set; } = string.Empty;
    }
}
