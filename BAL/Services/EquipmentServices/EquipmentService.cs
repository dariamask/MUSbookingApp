using BAL.Dto.Equipment;
using BAL.Mapper;
using DAL.Data.Entities;
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
        public async Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken)
        {
            // TODO валидация

            var equipment = new Equipment
            {
                Name = dto.Name,
                Amount = dto.Amount,
                Price = dto.Price,
            };

            await _equipmentRepository.CreateEquipmentAsync(equipment, cancellationToken);

            return equipment.MapToResponse();
        }
    }
}
