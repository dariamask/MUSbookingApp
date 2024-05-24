using BAL.Dto.Order;
using BAL.Mapper;
using BAL.Services.Validation.Result;
using DAL.Data.Entities;
using DAL.Repository.OrderRepo;
using FluentResults;
using static System.Net.Mime.MediaTypeNames;

namespace BAL.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result<OrderDto>> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken)
        {
            // TODO валидация

            var order = new Order
            {
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                Price = dto.Price
            };

            await _orderRepository.CreateOrderAsync(order, cancellationToken);

            return order.MapToResponse();
        }

        public async Task<Result> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);

            if(order is null)
            {
                return Result.Fail(Errors.OrderDoesntExist);
            }
            
            await _orderRepository.DeleteOrderAsync(order, cancellationToken);

            return Result.Ok();
        }

        public async Task<Result<List<OrderDto>>> GetOrderWithPaginationAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<OrderDto>> UpdateOrderAsync(Guid orderId, OrderUpdateDto updatedOrder, CancellationToken cancellationToken)
        {
            //TODO валидация 

            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);

            if (order is null)
            {
                return Result.Fail(Errors.OrderDoesntExist);
            }

            order.Description = updatedOrder.Description ?? order.Description;
            order.Price = updatedOrder.Price ?? order.Price;
            order.UpdatedAt = DateTime.Now;

            await _orderRepository.UpdateOrderAsync(order, cancellationToken);

            return order.MapToResponse();
        }
    }
}
