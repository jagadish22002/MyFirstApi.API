using System;
using System.Collections.Generic;

namespace MyFirstApi.DTOs
{
    public class CreateOrderDto
    {
        public List<Guid> ProductIds { get; set; } = new();
    }
}
