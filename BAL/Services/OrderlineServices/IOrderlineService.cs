using BAL.Dto.OrderDtos;
using BAL.Dto.OrderlineDtos;
using DAL.Data.Entities;
using FluentResults;

namespace BAL.Services.OrderlineServices
{
    public interface IOrderlineService
    {
        public Task<Result<OrderLine>[]> CreateOrderlinesAsync(List<OrderlineCreateDto> orderlinesRequest, Guid orderId, CancellationToken cancellationToken);
        public Task<Result<OrderLine>> UpdateOrderlineAsync(OrderlineUpdateDto orderlineRequest, Order order, CancellationToken cancellationToken);
        public Task<Result> DeleteOrderlinesAsync(List<OrderLine> orderlinesRequest, CancellationToken cancellationToken);
    }
}
