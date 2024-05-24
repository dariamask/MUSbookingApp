using BAL.Dto.Order;
using DAL.Repository.OrderRepo;
using FluentResults;

namespace BAL.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<Result<OrderDto>> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<OrderDto>> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<OrderDto>>> GetOrderWithPaginationAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<OrderDto>> UpdateOrderAsync(Guid orderId, OrderUpdateDto updatedOrder, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
