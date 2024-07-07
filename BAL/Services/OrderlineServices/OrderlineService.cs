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
        public async Task<Result<OrderLine>[]> CreateOrderlinesAsync(List<OrderlineCreateDto> orderlinesRequest, Guid orderId, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<Result<OrderLine>>>();

            foreach (var orderEquipment in orderlinesRequest)
            {
                tasks.Add(ProcessOrderEquipmentAsync(orderEquipment));             
            }                
            
            async Task<Result<OrderLine>> ProcessOrderEquipmentAsync(OrderlineCreateDto orderEquipment)
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

            return await Task.WhenAll(tasks);
        }
          
        public async Task<Result<List<OrderLine>>> UpdateOrderlineAsync(List<OrderlineUpdateDto> orderlinesRequest, Order order, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var orderlines = new List<OrderLine>();

            foreach (var orderEquipment in orderlinesRequest)
            {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderEquipment.EquipmentId, cancellationToken);

                if (equipment is null)
                {
                    errors.Add(Errors.EquipmentDoesntExist(orderEquipment.EquipmentId));
                    continue;
                }

                if (equipment.Amount < orderEquipment.Quantity)
                {
                    errors.Add(Errors.NotEnough(equipment));
                    continue;
                }

                var orderline = new OrderLine
                {
                    //OrderId = orderId,
                    EquipmentId = equipment.Id,
                    Amount = orderEquipment.Quantity,
                    Price = equipment.Price
                };

                await _equipmentService.SubstractFromTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);
                await _orderlineRepository.CreateOrderLineAsync(orderline, cancellationToken);

                orderlines.Add(orderline);
            }

            return errors.Count == 0 ? orderlines : Result.Fail(errors);
        }
        public async Task<Result> DeleteOrderlineAsync(List<OrderLine> orderlines, CancellationToken cancellationToken)
        {
            foreach(var orderline in orderlines)
            {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderline.EquipmentId, cancellationToken);

                await _equipmentService.AddToTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);
            }

            return Result.Ok();
        }
        public async Task<Result> DeleteOrderlineAsync(OrderLine orderline, CancellationToken cancellationToken)
        {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderline.EquipmentId, cancellationToken);

                await _equipmentService.AddToTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);

            return Result.Ok();
        }
    }
}
