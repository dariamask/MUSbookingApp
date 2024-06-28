
namespace BAL.Dto.EquipmentDtos
{
    public record EquipmentToOrderCreateDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
