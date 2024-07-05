using BAL.Dto.OrderlineDtos;
using DAL.Data.Entities;
using FluentResults;
using System.Threading;

namespace BAL.Services.OrderlineServices
{
    public interface IOrderlineService
    {
        public Task<Result<List<OrderLine>>> CreateOrderlinesAsync(List<OrderlineCreateDto> orderlinesRequest, Guid orderId, CancellationToken cancellationToken);
        public Task<Result> DeleteOrderlineAsync(List<OrderLine> orderlinesRequest, CancellationToken cancellationToken);
    }
}
