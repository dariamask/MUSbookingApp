
namespace DAL.Data.Entities
{
    public class OrderLine
    {
        public Guid OrderId { get; set; }
        public Guid EquipmentId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
