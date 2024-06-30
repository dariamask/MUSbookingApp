using BAL.Dto.OrderDtos;
using BAL.Mapper;
using BAL.Services.EquipmentServices;
using BAL.Services.OrderlineServices;
using BAL.Validation.Result;
using DAL.Data.Entities;
//using DAL.Data.EntitiesConfiguration;
using DAL.Repository.EquipmentRepo;
using DAL.Repository.OrderLineRepo;
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
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IOrderlineService _orderlineService;
        private readonly IEquipmentService _equipmentService;
        private readonly IValidator<OrderCreateDto> _createValidator;
        private readonly IValidator<OrderUpdateDto> _updateValidator;
        
        public OrderService(IOrderRepository orderRepository,
            IEquipmentRepository equipmentRepository,
            IValidator<OrderCreateDto> createValidator,
            IValidator<OrderUpdateDto> updateValidator,
            IEquipmentService equipmentService,
            IOrderlineService orderlineService,
            ILogger<OrderService> logger
            )

        {
            _orderRepository = orderRepository;
            _equipmentRepository = equipmentRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _equipmentRepository = equipmentRepository;
            _equipmentService = equipmentService;
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

            using var transaction = _orderRepository.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                if (request.EquipmentToOrder is not null)
                {
                    var orderlines = await _orderlineService.GetOrderlinesAsync(request.EquipmentToOrder, order.Id, cancellationToken);

                    if (orderlines.IsFailed)
                    {
                        transaction.Rollback();
                        return Result.Fail(orderlines.Errors);
                    }

                    orderlines = await _equipmentService.SubstractAmountOfEquipmentAsync(orderlines.Value, cancellationToken);

                    if (orderlines.IsFailed)
                    {
                        transaction.Rollback();
                        return Result.Fail(orderlines.Errors);
                    }
                    order.OrderLine = orderlines.Value;
                    order.Price = GetOrderPrice(order.OrderLine, cancellationToken);
                }
                else
                {
                    order.Price = 0;
                    order.OrderLine = null;
                }

                await _orderRepository.CreateOrderAsync(order, cancellationToken);
                
                transaction.Commit();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured: {ErrorMessage}", ex.Message);
                transaction.Rollback();
            }

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

            //order.Description = updatedOrder.Description ?? order.Description;
            //order.Price = updatedOrder.Price ?? order.Price;
            //order.UpdatedAt = DateTime.Now;

            await _orderRepository.UpdateOrderAsync(order, cancellationToken);

            return Result.Ok();
        }

        public decimal GetOrderPrice(List<OrderLine> orderLines, CancellationToken cancellationToken)
        {
            return orderLines.Sum(x => x.Price * x.Amount);
        }
    }
}
