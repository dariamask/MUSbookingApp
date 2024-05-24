using BAL.Dto.Order;
using BAL.Services.OrderServices;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace MUSbookingApp.Controllers
{        
    [Route("order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Вывод списка всех заказов, отсортированных по дате создания с пагинацией
        public async Task<ActionResult<OrderDto>> GetOrderPagination(CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderWithPaginationAsync(cancellationToken);
            return result.ToActionResult();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]        
        //Создание заказа
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto request, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateOrderAsync(request, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Обновление заказа
        public async Task<ActionResult<OrderDto>> Update(Guid orderId,
            [FromBody] OrderUpdateDto request, CancellationToken cancellationToken)
        {
            var result = await _orderService.UpdateOrderAsync(orderId, request, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //Удаление заказа
        public async Task<ActionResult> Delete (Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _orderService.DeleteOrderAsync(orderId, cancellationToken);
            return result.ToActionResult();
        }
    }
}
