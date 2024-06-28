using BAL.Dto.EquipmentDtos;
using BAL.Dto.OrderDtos;
using DAL.Data.Entities;
using System;


namespace BAL.Mapper
{
    public static class EquipmentToOrderMapper
    {
        public static EquipmentToOrderDto MapToResponse(this OrderLine orderLine)
        {
            return new EquipmentToOrderDto
            {
                Id = orderLine.EquipmentId,
                Quantity = orderLine.Amount,
                Price = orderLine.Price
            };
        }
        public static List<EquipmentToOrderDto> MapToResponse(this IEnumerable<OrderLine> orderLines)
        {
            return orderLines.Select(MapToResponse).ToList();
        }
    }
}
