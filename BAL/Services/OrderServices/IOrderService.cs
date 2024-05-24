﻿
using BAL.Dto.Order;
using FluentResults;

namespace BAL.Services.OrderServices
{
    public interface IOrderService
    {
        public Task<Result<OrderDto>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);
        public Task<Result<List<OrderDto>>> GetOrderWithPaginationAsync(CancellationToken cancellationToken);
        public Task<Result<OrderDto>> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken);
        public Task<Result<OrderDto>> UpdateOrderAsync(Guid orderId, OrderUpdateDto updatedOrder, CancellationToken cancellationToken);
        public Task<Result> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
