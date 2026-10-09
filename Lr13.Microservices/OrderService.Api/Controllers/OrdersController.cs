using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Models;
using OrderService.Api.Repositories;
using OrderService.Api.Sagas;

namespace OrderService.Api.Controllers
{
    /// <summary>
    /// DTO запроса на запуск Saga оформления заказа.
    /// simulateStockFailure = true шаг 3 упадёт (для демонстрации отката).
    /// </summary>
    public sealed record CheckoutRequest(string Name, decimal Amount, bool SimulateStockFailure = false);

    /// <summary>
    /// Контроллер OrderService.
    /// Точка входа в Saga: POST /orders/checkout запускает оркестратор.
    /// GET /orders возвращает текущее состояние «БД» заказов.
    /// </summary>
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _orders;
        private readonly CheckoutSaga _saga;

        public OrdersController(OrderRepository orders, CheckoutSaga saga)
        {
            _orders = orders;
            _saga = saga;
        }

        /// <summary>Запуск Saga оформления заказа.</summary>
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            string log = await _saga.CheckoutAsync(request.Name, request.Amount, request.SimulateStockFailure);
            return Ok(new { log });
        }

        /// <summary>Просмотр текущих заказов.</summary>
        [HttpGet]
        public IActionResult All() => Ok(_orders.All);
    }
}
