using BAL.Dto.OrderDtos;
using FluentResults;

namespace BAL.Services.OrderServices
{
    public interface IOrderService
    {
        public Task<Result<List<OrderDto>>> GetOrderWithPaginationAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        public Task<Result<string>> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken);
        public Task<Result<string>> UpdateOrderAsync(Guid orderId, OrderUpdateDto updatedOrder, CancellationToken cancellationToken);
        public Task<Result<string>> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
