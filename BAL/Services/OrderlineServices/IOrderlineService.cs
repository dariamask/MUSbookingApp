
using BAL.Dto.EquipmentDtos;
using DAL.Data.Entities;
using FluentResults;

namespace BAL.Services.OrderlineServices
{
    public interface IOrderlineService
    {
        public Task<Result<List<OrderLine>>> CreateOrderlineAsync(List<OrderlineCreateDto> orderlinesRequest, Guid orderId, CancellationToken cancellationToken);
    }
}
