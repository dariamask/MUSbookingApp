namespace BAL.Dto.OrderlineDtos
{
    public record OrderlineCreateDto
    {
        public Guid EquipmentId { get; set; }
        public int Quantity { get; set; }
    }
}
