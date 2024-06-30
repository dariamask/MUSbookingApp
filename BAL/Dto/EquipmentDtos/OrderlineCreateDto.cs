
namespace BAL.Dto.EquipmentDtos
{
    public record OrderlineCreateDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
