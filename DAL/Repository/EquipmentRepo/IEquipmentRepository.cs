using DAL.Data.Entities;

namespace DAL.Repository.EquipmentRepo
{
    public interface IEquipmentRepository
    {
        Task CreateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
        Task<bool> IsEquipmentUniqie(string equipmentName, CancellationToken cancellationToken);
        Task<Equipment> GetEquipmentByIdAsync(Guid equipmentId, CancellationToken cancellationToken);
        Task UpdateAmountOfEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
        Task<decimal> GetEquipmentPriceById(Guid Id, CancellationToken cancellationToken);
    }
}
