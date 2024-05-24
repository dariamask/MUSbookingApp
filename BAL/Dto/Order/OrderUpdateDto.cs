
namespace BAL.Dto.Order
{
    public record OrderUpdateDto
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
