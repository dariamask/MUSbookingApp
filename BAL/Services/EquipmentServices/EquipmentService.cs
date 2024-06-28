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

        public async Task SubstructAmountOfEquipmentAsync(List<OrderLine> orderLines, CancellationToken cancellationToken)
        {
            // TODO validation, check total amount

            
            /* var tasks = orderLines.Select(async orderLine =>
            {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderLine.EquipmentId, cancellationToken);
                equipment.Amount -= orderLine.Amount;   
                _equipmentRepository.UpdateAmountOfEquipmentAsync(equipment, cancellationToken);
            });
            await Task.WhenAll(tasks); */
            

            foreach (var orderLine in orderLines)
            {
                var equipment = await _equipmentRepository.GetEquipmentByIdAsync(orderLine.EquipmentId, cancellationToken);
                equipment.Amount -= orderLine.Amount;   
                await _equipmentRepository.UpdateAmountOfEquipmentAsync(equipment, cancellationToken);
            }
        }
    }
}
