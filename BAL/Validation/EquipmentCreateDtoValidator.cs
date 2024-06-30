using BAL.Dto.EquipmentDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class EquipmentCreateDtoValidator : AbstractValidator<EquipmentCreateDto>
    {
        public EquipmentCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().Must(x => x >= 0).WithMessage("Amount can't be negative");
        }
    }
}
