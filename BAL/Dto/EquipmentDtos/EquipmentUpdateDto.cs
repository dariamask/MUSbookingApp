
namespace BAL.Dto.EquipmentDtos
{
    public record EquipmentUpdateDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
