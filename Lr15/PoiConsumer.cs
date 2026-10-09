using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lr15;

/// <summary>
/// Consumer в паттерне Producer/Consumer.
/// Забирает точки из очереди и обрабатывает.
/// GetConsumingEnumerable завершится автоматически,
/// когда продюсер вызовет CompleteAdding и очередь опустеет.
/// </summary>
public sealed class PoiConsumer
{
    private readonly BlockingCollection<PointOfInterest> _queue;
    private readonly int _id;

    public PoiConsumer(BlockingCollection<PointOfInterest> queue, int id)
    {
        _queue = queue;
        _id = id;
    }

    public void Consume()
    {
        foreach (PointOfInterest poi in _queue.GetConsumingEnumerable())
        {
            // Имитация долгой обработки (внешний сервис, БД, сеть).
            Thread.Sleep(200);

            // Присваиваем рейтинг на основе координат.
            poi.Rating = Math.Round(3.0 + (poi.Latitude + poi.Longitude) % 2.0, 1);

            Console.WriteLine($"[Consumer #{_id}] обработана {poi.Name} -> рейтинг {poi.Rating}");
        }

        Console.WriteLine($"[Consumer #{_id}] завершил работу (очередь пуста).");
    }
}