﻿
namespace BAL.Dto.Equipment
{
    public record EquipmentCreateDto
    {
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
