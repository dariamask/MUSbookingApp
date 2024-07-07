using BAL.Dto.OrderDtos;
using BAL.Dto.OrderlineDtos;
using DAL.Data.Entities;
using FluentResults;

namespace BAL.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto MapToResponse(this Order order, List<string>? errors)
        {
            return new OrderDto
            {
                Id = order.Id,
                Description = order.Description,
                Price = order.Price,
                equipmentToOrderDtos = new OrderlineResultDto
                {
                    SuccessfulOrderLines = order.OrderLines?.MapToResponse(),
                    ErrorMessages = errors
                }
            };
        }
    }
}