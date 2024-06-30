using BAL.Dto.EquipmentDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderlineCreateDtoValidator : AbstractValidator<OrderlineCreateDto>
    {
        public OrderlineCreateDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);
        }
    }
}
