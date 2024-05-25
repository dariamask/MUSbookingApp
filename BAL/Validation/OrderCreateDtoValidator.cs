
using BAL.Dto.OrderDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x is not null);          
        }

    }
}
