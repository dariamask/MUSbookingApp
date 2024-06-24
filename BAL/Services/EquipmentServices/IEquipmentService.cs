using BAL.Dto.EquipmentDtos;
using DAL.Data.Entities;
using FluentResults;

namespace BAL.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        public Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken);

        public Task SubstructAmountOfEquipmentAsync(List<OrderLine> orderLines, CancellationToken cancellationToken);
    }
}
