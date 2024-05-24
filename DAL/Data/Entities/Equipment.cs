
namespace DAL.Data.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Order? Order { get; set; }
    }
}
