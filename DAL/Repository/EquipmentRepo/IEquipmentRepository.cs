using DAL.Data.Entities;
using System.Threading;

namespace DAL.Repository.EquipmentRepo
{
    public interface IEquipmentRepository
    {
        Task<int> GetTotalAmountOfEquipmentAsync(Guid id, CancellationToken cancellationToken);
        Task CreateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
        Task<bool> IsEquipmentUniqie(string equipmentName, CancellationToken cancellationToken);
        Task<Equipment> GetEquipmentByIdAsync(Guid equipmentId, CancellationToken cancellationToken);
        Task UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
        Task<decimal> GetEquipmentPriceById(Guid Id, CancellationToken cancellationToken);
    }
}
