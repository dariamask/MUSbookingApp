using BAL.Dto.OrderDtos;
using BAL.Mapper;
using BAL.Services.EquipmentServices;
using BAL.Validation.Result;
using DAL.Data;
using DAL.Data.Entities;
//using DAL.Data.EntitiesConfiguration;
using DAL.Repository.EquipmentRepo;
using DAL.Repository.OrderLineRepo;
using DAL.Repository.OrderRepo;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAL.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentService _equipmentService;
        private readonly IValidator<OrderCreateDto> _createValidator;
        private readonly IValidator<OrderUpdateDto> _updateValidator;
        
        public OrderService(IOrderRepository orderRepository,
            IEquipmentRepository equipmentRepository,
            IValidator<OrderCreateDto> createValidator,
            IValidator<OrderUpdateDto> updateValidator,
            IOrderLineRepository orderLineRepository,
            IEquipmentService equipmentService,
            ILogger<OrderService> logger
            )

        {
            _orderRepository = orderRepository;
            _equipmentRepository = equipmentRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _orderLineRepository = orderLineRepository;
            _equipmentRepository = equipmentRepository;
            _equipmentService = equipmentService;
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
                foreach (var eq in request.EquipmentToOrder)
                {
                    var equipment = await _equipmentRepository.GetEquipmentByIdAsync(eq.Id, cancellationToken);

                    if (equipment == null)
                    {
                        return Result.Fail(Errors.EquipmentDoesntExists);
                    }

                    List<OrderLine> orderLines = request.EquipmentToOrder
                    .Select(equipmentToOrder => new OrderLine
                    {
                        OrderId = order.Id,
                        EquipmentId = eq.Id,
                        Amount = equipmentToOrder.Quantity
                    }).ToList();
                }

                
                    



                await _orderRepository.CreateOrderAsync(order, cancellationToken);

                if (request.EquipmentToOrder != null)
                {
                    List<OrderLine> orderLines = request.EquipmentToOrder
                    .Select(equipmentToOrder => new OrderLine
                    {
                        OrderId = order.Id,
                        EquipmentId = 
                        Amount = equipmentToOrder.Quantity
                    }).ToList();

                    

                    await _equipmentService.SubstructAmountOfEquipmentAsync(orderLines, cancellationToken);

                    order.OrderLine = orderLines;
                    order.Price = GetOrderPrice(orderLines);
                }
                else
                {
                    order.Price = 0;
                    order.OrderLine = null;
                }

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

        public decimal GetOrderPrice(List<OrderLine> orderLines)
        {
            return orderLines.Sum(x => x.Price * x.Amount);
        }
    }
}
