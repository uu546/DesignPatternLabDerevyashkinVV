using OrderService.Api.Models;

namespace OrderService.Api.Repositories;


/// <summary>
/// Repository в паттерне Repository.
/// Прослойка между доменной логикой и хранилищем заказов.
/// Клиент работает с репозиторием как с коллекцией объектов,
/// не зная, где и как они хранятся.
///
/// Инкапсулирует:
///   - само хранилище (List<Order>);
///   - генерацию Id;
///   - создание заказа (Create);
///   - удаление заказа (Remove);
///   - чтение всех заказов (All).
/// </summary>
public sealed class OrderRepository
{
    private readonly List<Order> _orders = new();
    private int _nextId = 1;

    /// <summary>Все заказы (только для чтения).</summary>
    public IReadOnlyList<Order> All => _orders;

    /// <summary>
    /// Создать заказ. Генерация Id — ответственность репозитория,
    /// а не бизнес-логики.
    /// </summary>
    public Order Create(string name, decimal amount)
    {
        Order order = new Order
        {
            Id = _nextId++,
            Name = name,
            Amount = amount
        };

        _orders.Add(order);
        return order;
    }

    /// <summary>
    /// Удалить заказ по Id. Возвращает true, если заказ был найден и удалён.
    /// </summary>
    public bool Remove(int id)
    {
        return _orders.RemoveAll(o => o.Id == id) > 0;
    }
}