using BAL.Dto.EquipmentDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class EquipmentToOrderCreateDtoValidator : AbstractValidator<EquipmentToOrderCreateDto>
    {
        public EquipmentToOrderCreateDtoValidator()
        {            
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        }
    }
}
