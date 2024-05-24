
namespace BAL.Dto.Order
{
    public record OrderCreateDto
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
