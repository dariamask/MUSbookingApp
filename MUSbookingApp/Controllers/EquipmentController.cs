using BAL.Dto.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace MUSbookingApp.Controllers
{
    [Route("equipment")]
    [ApiController]
    public class EquipmentController : Controller
    { 
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Создание оборудования
        public async Task<ActionResult<EquipmentDto>> Create(CancellationToken cancellationToken)
        {
            return Ok();
        }


    }
}
