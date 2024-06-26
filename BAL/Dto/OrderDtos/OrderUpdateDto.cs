﻿
using BAL.Dto.EquipmentDtos;

namespace BAL.Dto.OrderDtos
{
    public record OrderUpdateDto
    {
        public string? Description { get; set; }
        public List<OrderlineCreateDto>? EquipmentToOrder { get; set; }
    }
}
