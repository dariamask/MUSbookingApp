using BAL.Dto.EquipmentDtos;
using FluentResults;

namespace BAL.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        public Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken);
        public Task<Result<EquipmentDto>> UpdateAmountOfEquipmentAsync(EquipmentUpdateDto dto, CancellationToken cancellationToken);
    }
}
