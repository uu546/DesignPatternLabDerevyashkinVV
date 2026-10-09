namespace PaymentService.Api.Models;

/// <summary>
/// Модель платежа.
/// Хранится отдельно от заказов — своя БД.
/// </summary>
public sealed class Payment
{
    public int Id { get; init; }
    public int OrderId { get; init; }
    public decimal Amount { get; init; }
}