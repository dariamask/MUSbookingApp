﻿using BAL.Dto.OrderlineDtos;

namespace BAL.Dto.OrderDtos
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<OrderlineDto>? equipmentToOrderDtos { get; set; }
    }
}
