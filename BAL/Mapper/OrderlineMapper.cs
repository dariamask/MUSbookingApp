using BAL.Dto.OrderDtos;
using BAL.Dto.OrderlineDtos;
using DAL.Data.Entities;
using System;


namespace BAL.Mapper
{
    public static class OrderlineMapper
    {
        public static OrderlineDto MapToResponse(this OrderLine orderLine)
        {
            return new OrderlineDto
            {
                EquipmentId = orderLine.EquipmentId,
                Quantity = orderLine.Amount,
                Price = orderLine.Price
            };
        }
        public static List<OrderlineDto> MapToResponse(this IEnumerable<OrderLine> orderLines)
        {
            return orderLines.Select(MapToResponse).ToList();
        }
    }
}
