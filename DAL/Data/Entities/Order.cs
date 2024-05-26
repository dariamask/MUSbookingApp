
namespace DAL.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public decimal Price { get; set; }
        public List<OrderEquipment>? OrderLine { get; set; }
    }
}
