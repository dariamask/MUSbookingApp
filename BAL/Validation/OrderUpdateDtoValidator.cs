using BAL.Dto.OrderDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator() 
        {
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x is not null);

            RuleForEach(x => x.EquipmentToOrder)
                .SetValidator(new OrderlineUpdateDtoValidator());
        }
    }
}
