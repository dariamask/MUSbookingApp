
using BAL.Dto.EquipmentDtos;
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
        public async Task<Result<List<OrderLine>>> CreateOrderlineAsync(List<OrderlineCreateDto> orderlinesRequest, Guid orderId, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var orderlines = new List<OrderLine>();

            foreach (var orderEquipment in orderlinesRequest)
            {
               var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderEquipment.Id, cancellationToken);

                if (equipment is null)
                {
                    errors.Add(Errors.EquipmentDoesntExist + orderEquipment.Id);
                    continue;
                }
                
                if (equipment.Amount < orderEquipment.Quantity)
                {
                    errors.Add(equipment.Name + equipment.Id + Errors.NotEnough + equipment.Amount);
                    continue;
                }

                var orderline = new OrderLine
                {
                    EquipmentId = equipment.Id,
                    Amount = orderEquipment.Quantity,
                    Price = equipment.Price
                };

                await _equipmentService.SubstractFromTotalAmountOfEquipmentAsync(orderline, equipment, cancellationToken);

                orderlines.Add(orderline);
            }

            return errors.Count == 0 ? orderlines : Result.Fail(errors);
        }
    }
}
