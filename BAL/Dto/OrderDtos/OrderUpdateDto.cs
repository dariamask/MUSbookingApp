using BAL.Dto.OrderlineDtos;

namespace BAL.Dto.OrderDtos
{
    public record OrderUpdateDto
    {
        public Guid OrderId { get; set; }
        public string? Description { get; set; }
        public List<OrderlineUpdateDto>? EquipmentToOrder { get; set; }
    }
}
