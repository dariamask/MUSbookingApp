namespace BAL.Dto.OrderlineDtos
{
    public record class OrderlineDto
    {
        public Guid EquipmentId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}