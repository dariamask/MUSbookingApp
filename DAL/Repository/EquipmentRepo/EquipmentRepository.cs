
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
        public async Task CreateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            _context.Add(equipment);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> IsEquipmentUniqie(string equipmentName, CancellationToken cancellationToken)
        {
            return await _context.Equipments.AnyAsync(x => x.Name == equipmentName);
        }
    }
}
