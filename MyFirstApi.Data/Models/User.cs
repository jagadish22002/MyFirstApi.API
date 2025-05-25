
using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.Data
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid(); // Primary key as UUID

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string MobileNumber { get; set; } = string.Empty;
    }
}
