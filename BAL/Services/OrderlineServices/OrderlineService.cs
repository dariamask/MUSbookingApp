using BAL.Dto.OrderlineDtos;
using BAL.Services.EquipmentServices;
using BAL.Validation.Result;
using DAL.Data.Entities;
using DAL.Repository.EquipmentRepo;
using DAL.Repository.OrderLineRepo;
using FluentResults;

namespace BAL.Services.OrderlineServices
{
    public class OrderlineService : IOrderlineService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentService _equipmentService;
        private readonly IOrderlineRepository _orderlineRepository;

        public OrderlineService(
            IEquipmentRepository equipmentRepository,
            IOrderlineRepository orderlineRepository,
            IEquipmentService equipmentService)
        {
            _equipmentRepository = equipmentRepository;
            _orderlineRepository = orderlineRepository;
            _equipmentService = equipmentService;
        }

        public async Task<Result<OrderLine>> CreateOrderlineAsync(OrderlineCreateDto orderEquipment, Guid orderId, CancellationToken cancellationToken)
        {
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderEquipment.EquipmentId, cancellationToken);

            if (equipment is null)
            {
                return Result.Fail(Errors.EquipmentDoesntExist(orderEquipment.EquipmentId));
            }

            if (equipment.Amount < orderEquipment.Quantity)
            {
                return Result.Fail(Errors.NotEnough(equipment));
            }

            var orderline = new OrderLine
            {
                OrderId = orderId,
                EquipmentId = equipment.Id,
                Amount = orderEquipment.Quantity,
                Price = equipment.Price
            };

            await _equipmentService.SubstractFromTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);
            await _orderlineRepository.CreateOrderLineAsync(orderline, cancellationToken);

            return orderline;
        }
        public async Task<Result<OrderLine>[]> CreateOrderlinesAsync(List<OrderlineCreateDto> orderlinesRequest, Guid orderId, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<Result<OrderLine>>>();

            foreach (var orderEquipment in orderlinesRequest)
            {
                tasks.Add(CreateOrderlineAsync(orderEquipment, orderId, cancellationToken));             
            }                           

            return await Task.WhenAll(tasks);
        }
        public async Task<Result<OrderLine>> UpdateOrderlineAsync(OrderlineUpdateDto orderlineRequest, Order order, CancellationToken cancellationToken)
        {
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderlineRequest.EquipmentId, cancellationToken);

            if (equipment is null)
            {
                return Result.Fail(Errors.EquipmentDoesntExist(orderlineRequest.EquipmentId));
            }
            if (equipment.Amount < orderlineRequest.Quantity)
            {
                return Result.Fail(Errors.NotEnough(equipment));
            }

            // поправить обновление - сравнить с количеством из существующего заказа

            var orderline = new OrderLine
            {
                OrderId = order.Id,
                EquipmentId = equipment.Id,
                Amount = orderlineRequest.Quantity,
                Price = equipment.Price
            };

            await _equipmentService.SubstractFromTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);
            await _orderlineRepository.UpdateOrderLineAsync(orderline, cancellationToken);

            return orderline;
        }
        public async Task<Result<OrderLine>[]> UpdateOrderlinesAsync(Order order, List<OrderlineUpdateDto> orderlinesRequest, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<Result<OrderLine>>>();

            // сравнить на одинаковость
            // сравнить на новые заказы
            // сравнить на удаленные заказы
            // сравнить на заказы без изменений

            foreach (var orderline in orderlinesRequest)
            {
                tasks.Add(UpdateOrderlineAsync(orderline, order, cancellationToken));
            }

            return await Task.WhenAll(tasks);
        }
        public async Task<Result> DeleteOrderlinesAsync(List<OrderLine> orderlines, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach(var orderline in orderlines)
            {
                tasks.Add(DeleteOrderlineAsync(orderline, cancellationToken));
            }
                
            await Task.WhenAll(tasks);

            return Result.Ok();
        }
        public async Task DeleteOrderlineAsync(OrderLine orderline, CancellationToken cancellationToken)
        {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderline.EquipmentId, cancellationToken);

                await _equipmentService.AddToTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);
        }
    }
}
