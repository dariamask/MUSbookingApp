using BAL.Dto.OrderlineDtos;
using DAL.Data.Entities;

namespace BAL.Mapper
{
    public static class OrderlineUpdateDtoMapper
    {
        public static OrderlineCreateDto MapToOrderlineCreateDto(this OrderlineUpdateDto orderLineUpdateDto)
        {
            return new OrderlineCreateDto
            {
                EquipmentId = orderLineUpdateDto.EquipmentId,
                Quantity = orderLineUpdateDto.Quantity,
            };
        }
        public static List<OrderlineCreateDto> MapToOrderlineCreateDto(this IEnumerable<OrderlineUpdateDto> orderLinesToCreate)
        {
            return orderLinesToCreate.Select(MapToOrderlineCreateDto).ToList();
        }
    }
}
