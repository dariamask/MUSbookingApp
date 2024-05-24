using Microsoft.AspNetCore.Mvc;

namespace MUSbookingApp.Controllers
{        
    [Route("order")]
    [ApiController]
    public class OrderController : Controller
    {
        //Создание оборудования
        //Создание заказа
        //Обновление заказа
        //Удаление заказа
        //Вывод списка всех заказов, отсортированных по дате создания с пагинацией

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto request, CancellationToken cancellationToken)
        {
            var result = await _applicationService.CreateApplicationAsync(request, cancellationToken);

            return result.ToActionResult();
        }
    }
}
