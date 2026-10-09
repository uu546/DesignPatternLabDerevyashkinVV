namespace OrderService.Api.HttpClients;

/// <summary>
/// HTTP-клиент к PaymentService.
/// Инкапсулирует три удалённые операции:
///   ChargeAsync         — списать оплату (шаг 2 Saga);
///   CancelChargeAsync   — компенсация шага 2 (отменить списание);
///   ReserveStockAsync   — шаг 3 Saga (имитация склада).
/// </summary>
public sealed class PaymentClient
{
    private readonly HttpClient _http;

    public PaymentClient(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("http://localhost:5002");
    }

    /// <summary>Шаг 2 Saga: списать оплату в PaymentService.</summary>
    public async Task<int> ChargeAsync(int orderId, decimal amount)
    {
        HttpResponseMessage response = await _http.PostAsJsonAsync("/payments/payments", new { orderId, amount });
        response.EnsureSuccessStatusCode();

        ChargeResponse? payload = await response.Content.ReadFromJsonAsync<ChargeResponse>();
        return payload!.PaymentId;
    }

    /// <summary>Компенсация шага 2: отменить списание (вернуть оплату).</summary>
    public async Task CancelChargeAsync(int paymentId)
    {
        HttpResponseMessage response = await _http.DeleteAsync($"/payments/payments/{paymentId}");
        response.EnsureSuccessStatusCode();
    }

    /// <summary>Шаг 3 Saga: зарезервировать товар (имитация склада).</summary>
    public async Task ReserveStockAsync(int orderId, bool simulateFailure)
    {
        HttpResponseMessage response = await _http.PostAsJsonAsync("/payments/stock/reserve", new { orderId, simulateFailure });
        response.EnsureSuccessStatusCode();
    }

    private sealed record ChargeResponse(int PaymentId);
}