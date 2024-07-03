using BAL.Dto.OrderDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x is not null);

            RuleForEach(x => x.EquipmentToOrder)
                .SetValidator(new OrderlineCreateDtoValidator());
        }

    }
}
