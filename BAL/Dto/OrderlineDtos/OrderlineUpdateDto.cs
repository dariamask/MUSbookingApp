
namespace BAL.Dto.OrderlineDtos
{
    public class OrderlineUpdateDto
    {
        public Guid OrderId { get; set; }
        public Guid EquipmentId { get; set; }
        public int Quantity { get; set; }
    }
}
