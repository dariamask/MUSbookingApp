
using BAL.Dto.EquipmentDtos;

namespace BAL.Dto.OrderDtos
{
    public record OrderUpdateDto
    {
        public string? Description { get; set; }
        public List<EquipmentToOrderCreateDto>? EquipmentToOrder { get; set; }
    }
}
