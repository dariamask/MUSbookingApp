using BAL.Dto.OrderDtos;
using DAL.Data.Entities;

namespace BAL.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto MapToResponse(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Description = order.Description,
                Price = order.Price,
                equipmentToOrderDtos = order.OrderLines?.MapToResponse(),
            };
        }

        public static List<OrderDto> MapToResponse(this IEnumerable<Order> orders)
        {
            return orders.Select(MapToResponse).ToList();
        }
    }
}
