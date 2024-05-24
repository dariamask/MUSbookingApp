using BAL.Dto.Equipment;
using DAL.Repository.EquipmentRepo;
using FluentResults;

namespace BAL.Services.EquipmentServices
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        public Task<Result<EquipmentService>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
