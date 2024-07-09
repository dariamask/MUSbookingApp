using BAL.Dto;
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

        public async Task<Result<string>> CreateOrderAsync(OrderCreateDto request, CancellationToken cancellationToken)
        {
            var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.Errors.Select(failure => failure.ErrorMessage));
            }

            var response = new CreateResponse();

            using var transaction = _orderRepository.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                var order = new Order
                {
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                };

                await _orderRepository.CreateOrderAsync(order, cancellationToken);

                if (request.EquipmentToOrder is not null)
                {
                    var orderlines = await _orderlineService.CreateOrderlinesAsync(request.EquipmentToOrder, order.Id, cancellationToken);

                    response.SuccessfulOrderlines = order.OrderLines.Count();
                    response.TotalOrderlineRequestCount = request.EquipmentToOrder.Count();
                    response.Errors = orderlines
                        .Where(x => x.IsFailed)
                        .SelectMany(x => x.Errors
                        .Select(x => x.Message)).ToList();
                }

                order.Price = GetOrderPrice(order.OrderLines);

                await _orderRepository.UpdateOrderAsync(order, cancellationToken);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured: {ErrorMessage}", ex.Message);
                transaction.Rollback();
            }

            return response.GetResponseMessage();
        }

        public async Task<Result<string>> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            using var transaction = _orderRepository.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);

                if (order is null)
                {
                    return Result.Fail(Errors.OrderDoesntExist);
                }
                
                if(order.OrderLines is not null)
                {
                    await _orderlineService.DeleteOrderlinesAsync(order.OrderLines, cancellationToken);
                }

                await _orderRepository.DeleteOrderAsync(order, cancellationToken);

                transaction.Commit();

                return DeleteResponse.DeleteSuccessfuly;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting order {orderId}: {ErrorMessage}", orderId, ex.Message);
                
                transaction.Rollback();

                return Result.Fail(DeleteResponse.DeleteFailed);
            }
        }

        public async Task<Result<List<OrderDto>>> GetOrderWithPaginationAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            if(pageSize <= 0 || pageIndex <= 0)
            {
                return Result.Fail(Errors.PaginationParametersBelowZero);
            }

            var totalItems = await _orderRepository.GetTotalNumberOfOrders(cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if(totalPages < pageIndex)
            {
                return Result.Fail(Errors.EmptyPages(totalPages));
            }

            var orders = await _orderRepository.GetOrderPaginationByCreatedDateAsync(pageIndex, pageSize, cancellationToken);

            return orders.MapToResponse();
        }

        public async Task<Result<string>> UpdateOrderAsync(Guid orderId, OrderUpdateDto updatedOrder, CancellationToken cancellationToken)
        {
            var validationRezult = await _updateValidator.ValidateAsync(updatedOrder, cancellationToken);

            if (!validationRezult.IsValid)
            {
                return Result.Fail(validationRezult.Errors.Select(failure => failure.ErrorMessage));
            }

            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);

            if (order is null)
            {
                return Result.Fail(Errors.OrderDoesntExist);
            }

            

            if(updatedOrder.EquipmentToOrder is not null && order.OrderLines is not null)
            {
                //orders to delete
                var orderLinesToDelete = order.OrderLines
                    .Where(orderline => !updatedOrder.EquipmentToOrder
                    .Any(orderlineUpdate => orderlineUpdate.EquipmentId == orderline.EquipmentId))
                    .ToList();

                var deleteResult = await _orderlineService.DeleteOrderlinesAsync(orderLinesToDelete, cancellationToken);
                
                //orders to create
                var ordelinesToCreate = updatedOrder.EquipmentToOrder
                    .Where(updOrder => !order.OrderLines
                    .Any(orderline => orderline.EquipmentId == updOrder.EquipmentId))
                    .MapToOrderlineCreateDto()
                    .ToList();

                var createResult = await _orderlineService.CreateOrderlinesAsync(ordelinesToCreate, order.Id, cancellationToken);
            }
            
            





            //orders to update

            //orders no changes

            



            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.UpdateOrderAsync(order, cancellationToken);

            return Result.Ok();
        }

        public decimal GetOrderPrice(List<OrderLine>? orderLines)
        {
            return orderLines is null ? 0 : orderLines.Sum(x => x.Price * x.Amount);
        }
    }
}
