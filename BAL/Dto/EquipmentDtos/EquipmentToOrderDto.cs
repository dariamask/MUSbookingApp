﻿
namespace BAL.Dto.EquipmentDtos
{
    public record class EquipmentToOrderDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
