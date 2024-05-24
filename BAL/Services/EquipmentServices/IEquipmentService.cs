
using BAL.Dto.Equipment;
using BAL.Dto.Order;
using FluentResults;

namespace BAL.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        public Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken);
    }
}
