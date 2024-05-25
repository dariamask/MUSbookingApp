
using BAL.Dto.Order;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderCreateDtoValidation : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidation()
        {
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x is not null);
            RuleFor(x => x.Price).Must(x => x >= 0);
        }

    }
}
