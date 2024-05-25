using BAL.Dto.EquipmentDtos;
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
        
    }
}
