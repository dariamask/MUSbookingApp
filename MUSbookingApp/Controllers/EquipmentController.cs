using BAL.Dto.Equipment;
using BAL.Services.EquipmentServices;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace MUSbookingApp.Controllers
{
    [Route("equipment")]
    [ApiController]
    public class EquipmentController : Controller
    { 
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Создание оборудования
        public async Task<ActionResult<EquipmentDto>> Create(EquipmentCreateDto createDto, CancellationToken cancellationToken)
        {
            var result = await _equipmentService.CreateEquipmentAsync(createDto, cancellationToken);
            return result.ToActionResult();
        }


    }
}
