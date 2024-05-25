using BAL.Dto.EquipmentDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validation
{
    public class EquipmentCreateDtoValidator : AbstractValidator<EquipmentCreateDto>
    {
        public EquipmentCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().Must(x => x >= 0).WithMessage("Price can't be negative");
            RuleFor(x => x.Amount).NotEmpty().Must(x => x >= 0).WithMessage("Amount can't be negative");
        }
    }
}
