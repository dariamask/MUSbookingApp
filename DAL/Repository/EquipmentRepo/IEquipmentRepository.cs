using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.EquipmentRepo
{
    public interface IEquipmentRepository
    {
        Task CreateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
        Task<bool> IsEquipmentUniqie(string equipmentName, CancellationToken cancellationToken);
    }
}
