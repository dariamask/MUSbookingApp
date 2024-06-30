using BAL.Dto.EquipmentDtos;
using DAL.Data.Entities;
using FluentResults;

namespace BAL.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        public Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken);

        public Task<Result<List<OrderLine>>> SubstractAmountOfEquipmentAsync(List<OrderLine> orderLines, CancellationToken cancellationToken);

        public Task<decimal> GetEquipmentPriceById(Guid Id, CancellationToken cancellationToken);
    }
}
