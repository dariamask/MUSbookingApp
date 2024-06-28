using BAL.Dto.EquipmentDtos;
using FluentValidation;

namespace BAL.Validation
{
    public class EquipmentToOrderCreateDtoValidator : AbstractValidator<EquipmentToOrderCreateDto>
    {
        //public Guid Id { get; set; }
        //public int Quantity { get; set; }
        

        public EquipmentToOrderCreateDtoValidator()
        {            
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);

        }
    }
}
