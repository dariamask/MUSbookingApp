
namespace DAL.Data.Entities
{
    public class OrderLine
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
        public int Amount { get; set; }
    }
}
