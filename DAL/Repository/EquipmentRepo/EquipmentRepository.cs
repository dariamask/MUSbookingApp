using DAL.Data;
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.EquipmentRepo
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;
        public EquipmentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalAmountOfEquipmentAsync(Guid id, CancellationToken cancellationToken)
        {
            var equipment = await _context.Equipments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return equipment is null ? 0 : equipment.Amount;
        }
        public async Task CreateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            _context.Add(equipment);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            _context.Update(equipment);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsEquipmentUniqie(string equipmentName, CancellationToken cancellationToken)
        {
            return await _context.Equipments.AnyAsync(x => x.Name == equipmentName);
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId, CancellationToken cancellationToken)
        {
            return await _context.Equipments.FirstOrDefaultAsync(x => x.Id == equipmentId, cancellationToken);
        }

        public async Task<decimal> GetEquipmentPriceById(Guid EquipmentId, CancellationToken cancellationToken)
        {
            var equipment = await _context.Equipments.FirstOrDefaultAsync(x => x.Id == EquipmentId, cancellationToken);
            return equipment?.Price ?? 0;
        }
    }
}