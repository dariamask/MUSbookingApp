using BAL.Dto.OrderDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator() 
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x is not null);

            //RuleFor(x => x.Price)
            //    .Must(x => x >= 0)
            //    .WithMessage("Price cannot be negative")
            //    .When(x => x.Price is not null);
        }
    }
}
