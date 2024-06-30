
using BAL.Dto.EquipmentDtos;
using BAL.Services.EquipmentServices;
using BAL.Validation.Result;
using DAL.Data.Entities;
using DAL.Repository.EquipmentRepo;
using FluentResults;

namespace BAL.Services.OrderlineServices
{
    public class OrderlineService : IOrderlineService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public OrderlineService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        public async Task<Result<List<OrderLine>>> GetOrderlinesAsync(List<EquipmentToOrderCreateDto> equipmentToOrderRequest, Guid orderId, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var orderLines = new List<OrderLine>();

            // продолжать работу с тем валидной частью запроса, но сообщить и об ошибках - как?

            foreach (var equipmentRequest in equipmentToOrderRequest)
            {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentRequest.Id, cancellationToken);
                
                if (equipment is null)
                {
                    errors.Add(Errors.EquipmentDoesntExist + equipmentRequest.Id);
                    continue;
                }

                orderLines.Add(new OrderLine
                {
                    OrderId = orderId,
                    EquipmentId = equipmentRequest.Id,
                    Amount = equipmentRequest.Quantity
                });
            }

            return errors is null ? orderLines : Result.Fail(errors);
        }
    }
}
