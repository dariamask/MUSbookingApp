
namespace BAL.Dto.Equipment
{
    public record EquipmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
