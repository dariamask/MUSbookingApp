using BAL.Dto.EquipmentDtos;
using BAL.Mapper;
using BAL.Validation;
using BAL.Validation.Result;
using DAL.Data.Entities;
using DAL.Repository.EquipmentRepo;
using FluentResults;
using FluentValidation;
using System.Threading;

namespace BAL.Services.EquipmentServices
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IValidator<EquipmentCreateDto> _validator;
        public EquipmentService(IEquipmentRepository equipmentRepository,
            IValidator<EquipmentCreateDto> validator)
        {
            _equipmentRepository = equipmentRepository;
            _validator = validator;
        }
        public async Task<Result<EquipmentDto>> CreateEquipmentAsync(EquipmentCreateDto dto, CancellationToken cancellationToken)
        {
            //var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

            //if (!validationResult.IsValid)
            //{
            //    return Result.Fail(validationResult.Errors.Select(failure => failure.ErrorMessage));
            //}

            //if (await _equipmentRepository.IsEquipmentUniqie(dto.Name, cancellationToken))
            //{
            //    return Result.Fail(Errors.IsNotUnique);
            //}

            var equipment = new Equipment
            {
                Name = dto.Name,
                Amount = dto.Amount,
                Price = dto.Price,
            };

            await _equipmentRepository.CreateEquipmentAsync(equipment, cancellationToken);

            return equipment.MapToResponse();
        }

        public async Task<Result<List<OrderLine>>> SubstractAmountOfEquipmentAsync(List<OrderLine> orderLines, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            foreach (var orderLineEquipment in orderLines)
            {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderLineEquipment.EquipmentId, cancellationToken);

                if(equipment.Amount < orderLineEquipment.Amount)
                {
                    errors.Add(Errors.NotEnough + equipment.Name + equipment.Id);
                    continue;
                }

                equipment.Amount -= orderLineEquipment.Amount;
                await _equipmentRepository.UpdateEquipmentAsync(equipment, cancellationToken);
            }

            return errors is null ? orderLines : Result.Fail(errors);

        }
        public async Task<decimal> GetEquipmentPriceById(Guid id, CancellationToken cancellationToken)
        {
            return await _equipmentRepository.GetEquipmentPriceById(id, cancellationToken);
        }
    }
}
