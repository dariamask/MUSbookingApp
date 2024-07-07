using BAL.Dto.OrderDtos;
using BAL.Dto.OrderlineDtos;
using BAL.Mapper;
using BAL.Services.OrderlineServices;
using BAL.Validation.Result;
using DAL.Data.Entities;
using DAL.Repository.OrderRepo;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BAL.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderlineService _orderlineService;
        private readonly IValidator<OrderCreateDto> _createValidator;
        private readonly IValidator<OrderUpdateDto> _updateValidator;
        
        public OrderService(IOrderRepository orderRepository,
            IValidator<OrderCreateDto> createValidator,
            IValidator<OrderUpdateDto> updateValidator,
            IOrderlineService orderlineService,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _orderlineService = orderlineService;
            _logger = logger;
        }

        public async Task<Result<OrderDto>> CreateOrderAsync(OrderCreateDto request, CancellationToken cancellationToken)
        {
            var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.Errors.Select(failure => failure.ErrorMessage));
            }

            var order = new Order
            {
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
            };

            List<string>? failedOrderlineMessages = null;

            using var transaction = _orderRepository.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                await _orderRepository.CreateOrderAsync(order, cancellationToken);

                if (request.EquipmentToOrder is not null)
                {
                    var orderlines = await _orderlineService.CreateOrderlinesAsync(request.EquipmentToOrder, order.Id, cancellationToken);

                    failedOrderlineMessages = orderlines
                        .Where(ol => ol.IsFailed)
                        .SelectMany(ol => ol.Errors.Select(x => x.Message))
                        .ToList();
                }

                order.Price = GetOrderPrice(order.OrderLines, cancellationToken);

                await _orderRepository.UpdateOrderAsync(order, cancellationToken);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured: {ErrorMessage}", ex.Message);
                transaction.Rollback();
            }

            return order.MapToResponse(failedOrderlineMessages);
        }

        public async Task<Result> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);

            if(order is null)
            {
                return Result.Fail(Errors.OrderDoesntExist);
            }

            await _orderlineService.DeleteOrderlineAsync(order.OrderLines, cancellationToken);
            
            await _orderRepository.DeleteOrderAsync(order, cancellationToken);

            return Result.Ok();
        }

        public async Task<Result<List<OrderDto>>> GetOrderWithPaginationAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<OrderDto>> UpdateOrderAsync(Guid orderId, OrderUpdateDto updatedOrder, CancellationToken cancellationToken)
        {
            var result = await _updateValidator.ValidateAsync(updatedOrder, cancellationToken);

            if (!result.IsValid)
            {
                return Result.Fail(result.Errors.Select(failure => failure.ErrorMessage));
            }

            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);

            if (order is null)
            {
                return Result.Fail(Errors.OrderDoesntExist);
            }

            //order.Description = updatedOrder.Description ?? order.Description;
            //order.Price = updatedOrder.Price ?? order.Price;
            //order.UpdatedAt = DateTime.Now;

            await _orderRepository.UpdateOrderAsync(order, cancellationToken);

            return Result.Ok();
        }

        public decimal GetOrderPrice(List<OrderLine>? orderLines, CancellationToken cancellationToken)
        {
            return orderLines is null ? 0 : orderLines.Sum(x => x.Price * x.Amount);
        }
    }
}
