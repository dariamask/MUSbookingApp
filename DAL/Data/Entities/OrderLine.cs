
namespace DAL.Data.Entities
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid EquipmentId { get; set; }
        public int Amount { get; set; }
    }
}
