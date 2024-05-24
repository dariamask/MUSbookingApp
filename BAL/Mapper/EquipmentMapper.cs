using BAL.Dto.Equipment;
using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
