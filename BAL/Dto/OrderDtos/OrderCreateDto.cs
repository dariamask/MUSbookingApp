﻿
using BAL.Dto.EquipmentDtos;

namespace BAL.Dto.OrderDtos
{
    public class OrderCreateDto
    {
        public string? Description { get; set; }
        public List<OrderlineCreateDto>? EquipmentToOrder { get; set; }
    }
}
