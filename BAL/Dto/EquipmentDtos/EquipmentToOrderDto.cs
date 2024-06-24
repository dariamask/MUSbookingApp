using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BAL.Dto.EquipmentDtos
{
    public record EquipmentToOrderDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
