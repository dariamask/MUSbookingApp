
namespace BAL.Dto.OrderDtos
{
    public record OrderUpdateDto
    {
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
