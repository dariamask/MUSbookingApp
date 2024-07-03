using BAL.Dto.OrderlineDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderlineUpdateDtoValidator : AbstractValidator<OrderlineUpdateDto>
    {
        public OrderlineUpdateDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);
        }
    }
}
