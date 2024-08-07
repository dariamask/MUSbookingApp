﻿using BAL.Dto.OrderDtos;
using BAL.Services.OrderServices;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<OrderDto>> GetOrderPagination(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderWithPaginationAsync(pageIndex, pageSize, cancellationToken);
            return result.ToActionResult();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]        
        public async Task<ActionResult<string>> Create([FromBody] OrderCreateDto request, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateOrderAsync(request, cancellationToken);
            return result.ToActionResult();
        }   
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<string>> Update(Guid orderId,
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
