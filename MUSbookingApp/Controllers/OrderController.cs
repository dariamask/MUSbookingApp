using BAL.Dto.Order;
using Microsoft.AspNetCore.Mvc;

namespace MUSbookingApp.Controllers
{        
    [Route("order")]
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Вывод списка всех заказов, отсортированных по дате создания с пагинацией
        public async Task<ActionResult<OrderDto>> GetOrderPagination(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]        
        //Создание заказа
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto request, CancellationToken cancellationToken)
        {

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Обновление заказа
        public async Task<ActionResult<OrderDto>> Update(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Удаление заказа
        public async Task<ActionResult> Delete (CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
