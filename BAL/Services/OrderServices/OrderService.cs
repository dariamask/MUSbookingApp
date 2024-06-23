using BAL.Dto.OrderDtos;
using BAL.Mapper;
using BAL.Validation.Result;
using DAL.Data.Entities;
using DAL.Repository.EquipmentRepo;
using DAL.Repository.OrderRepo;
using FluentResults;
using FluentValidation;

namespace BAL.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IValidator<OrderCreateDto> _createValidator;
        private readonly IValidator<OrderUpdateDto> _updateValidator;
        public OrderService(IOrderRepository orderRepository,
            IEquipmentRepository equipmentRepository,
            IValidator<OrderCreateDto> createValidator,
            IValidator<OrderUpdateDto> updateValidator)
        {
            _orderRepository = orderRepository;
            _equipmentRepository = equipmentRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<Result<OrderDto>> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken)
        {
            var validationResult = await _createValidator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.Errors.Select(failure => failure.ErrorMessage));
            }

            var order = new Order
            {
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
            };

            //существует ли оборудование

            var orderToLine = new OrderLine()
            {


            };




            var order = new Order
            {
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
            };            

                        
            
            


            //await _orderRepository.CreateOrderAsync(order, cancellationToken);

            return Result.Fail("");
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
