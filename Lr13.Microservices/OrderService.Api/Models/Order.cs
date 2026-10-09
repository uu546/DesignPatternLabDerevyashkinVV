namespace OrderService.Api.Models;

/// <summary>
/// Модель заказа.
/// </summary>
public sealed class Order
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public decimal Amount { get; init; }
}