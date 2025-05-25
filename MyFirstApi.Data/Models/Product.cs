using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.Data
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // ✅ New column to track availability
        public bool IsAvailable { get; set; } = true; // Default: available
    }
}
