using OrderService.Api.HttpClients;
using OrderService.Api.Models;
using OrderService.Api.Repositories;

namespace OrderService.Api.Sagas;

/// <summary>
/// Оркестратор Saga оформления заказа.
///
/// Реализует распределённую транзакцию как последовательность
/// локальных транзакций с компенсациями при ошибке.
///
/// Схема:
///   один общий try/catch, внутри три вложенных try/catch —
///   по одному на каждый шаг Saga:
///     try { шаг 1: создать заказ }        catch { — }
///     try { шаг 2: списать оплату }        catch { отменить заказ }
///     try { шаг 3: зарезервировать товар } catch { отменить оплату, отменить заказ }
///
/// Компенсации выполняются в обратном порядке (LIFO):
/// сначала откатываем последний успешный шаг, потом предыдущий.
/// </summary>
public sealed class CheckoutSaga
{
    private readonly OrderRepository _orders;
    private readonly PaymentClient _payments;

    public CheckoutSaga(OrderRepository orders, PaymentClient payments)
    {
        _orders = orders;
        _payments = payments;
    }

    public async Task<string> CheckoutAsync(string name, decimal amount, bool simulateStockFailure)
    {
        List<string> log = new();

        try
        {
            // ===== Шаг 1: создать заказ =====
            Order order;
            try
            {
                order = _orders.Create(name, amount);
                log.Add($"Шаг 1: заказ #{order.Id} создан.");
            }
            catch (Exception ex)
            {
                log.Add($"Шаг 1 провалился: {ex.Message}");
                return string.Join("\n", log);
            }

            // ===== Шаг 2: списать оплату =====
            int paymentId;
            try
            {
                paymentId = await _payments.ChargeAsync(order.Id, amount);
                log.Add($"Шаг 2: оплата #{paymentId} списана.");
            }
            catch (Exception ex)
            {
                log.Add($"Шаг 2 провалился: {ex.Message}");

                _orders.Remove(order.Id);
                log.Add($"Компенсация шага 1: заказ #{order.Id} отменён.");

                return string.Join("\n", log);
            }

            // ===== Шаг 3: зарезервировать товар =====
            try
            {
                await _payments.ReserveStockAsync(order.Id, simulateStockFailure);
                log.Add($"Шаг 3: товар зарезервирован.");
            }
            catch (Exception ex)
            {
                log.Add($"Шаг 3 провалился: {ex.Message}");

                await _payments.CancelChargeAsync(paymentId);
                log.Add($"Компенсация шага 2: оплата #{paymentId} возвращена.");

                _orders.Remove(order.Id);
                log.Add($"Компенсация шага 1: заказ #{order.Id} отменён.");

                return string.Join("\n", log);
            }

            log.Add("Saga завершена успешно.");
            return string.Join("\n", log);
        }
        catch (Exception ex)
        {
            log.Add($"Общая ошибка Saga: {ex.Message}");
            return string.Join("\n", log);
        }
    }
}