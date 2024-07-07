
namespace BAL.Dto.OrderlineDtos
{
    public record OrderlineResultDto
    {
        public List<OrderlineDto>? SuccessfulOrderLines { get; set; }
        public List<string>? ErrorMessages { get; set; }
    }
}
