using Microsoft.AspNetCore.Mvc;
using PaymentService.Api.Models;

namespace PaymentService.Api.Controllers;

/// <summary>DTO запроса на списание оплаты (шаг 2 Saga).</summary>
public sealed record ChargeRequest(int OrderId, decimal Amount);

/// <summary>DTO запроса на резервирование товара (шаг 3 Saga).</summary>
public sealed record StockRequest(int OrderId, bool SimulateFailure);

/// <summary>
/// Контроллер PaymentService.
/// Предоставляет операции:
///   POST /payments         — списать оплату (шаг 2);
///   DELETE /payments/{id}  — компенсация шага 2 (CancelCharge — отменить списание);
///   POST /stock/reserve    — имитация склада (шаг 3);
///   GET /payments          — просмотр состояния «БД» платежей.
/// </summary>
[ApiController]
[Route("payments")]
public sealed class PaymentsController : ControllerBase
{
    private static readonly List<Payment> _payments = new();
    private static int _nextId = 1;

    /// <summary>Шаг 2 Saga: списать оплату.</summary>
    [HttpPost("payments")]
    public IActionResult Charge([FromBody] ChargeRequest request)
    {
        Payment payment = new Payment
        {
            Id = _nextId++,
            OrderId = request.OrderId,
            Amount = request.Amount
        };

        _payments.Add(payment);

        return Ok(new
        {
            paymentId = payment.Id
        });
    }

    /// <summary>Компенсация шага 2: CancelCharge — отменить списание (вернуть оплату).</summary>
    [HttpDelete("payments/{id}")]
    public IActionResult CancelCharge([FromRoute] int id)
    {
        _payments.RemoveAll(p => p.Id == id);

        return Ok();
    }

    /// <summary>Просмотр платежей (для проверки компенсаций).</summary>
    [HttpGet("payments")]
    public IActionResult All() => Ok(_payments);

    /// <summary>
    /// Шаг 3 Saga: имитация резервирования товара на складе.
    /// При simulateFailure=true возвращает 400 — это триггер компенсаций.
    /// </summary>
    [HttpPost("stock/reserve")]
    public IActionResult ReserveStock([FromBody] StockRequest request)
    {
        if (request.SimulateFailure)
        {
            return BadRequest(new { error = "Склад недоступен (симуляция)." });
        }

        return Ok(new { reserved = true });
    }
}
