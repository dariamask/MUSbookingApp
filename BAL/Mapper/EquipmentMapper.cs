using BAL.Dto.EquipmentDtos;
using BAL.Dto.OrderDtos;
using DAL.Data.Entities;

namespace BAL.Mapper
{
    public static class EquipmentMapper
    {
        public static EquipmentDto MapToResponse (this Equipment equipment)
        {
            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Amount = equipment.Amount,
                Price = equipment.Price,
            };
        }
        public static List<EquipmentDto> MapToResponse(this IEnumerable<Equipment> equipments)
        {
            return equipments.Select(MapToResponse).ToList();
        }
    }
}
