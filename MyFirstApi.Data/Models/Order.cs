
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstApi.Data
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string OrderStatus { get; set; } = "OnWay"; // ✅ "OnWay" or "Cancelled"

        // ✅ This fixes the error
        public List<OrderProduct> OrderProducts { get; set; } = new();
    }
}
