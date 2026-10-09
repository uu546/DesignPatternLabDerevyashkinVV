using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lr15;

/// <summary>
/// Producer в паттерне Producer/Consumer.
/// Генерирует точки и кладёт их в очередь.
/// После завершения вызывает CompleteAdding —
/// это сигнал консьюмерам, что новых задач не будет.
/// </summary>
public sealed class PoiProducer
{
    private readonly BlockingCollection<PointOfInterest> _queue;

    public PoiProducer(BlockingCollection<PointOfInterest> queue)
    {
        _queue = queue;
    }

    public void Produce(int count)
    {
        Random random = new Random(42);

        for (int i = 1; i <= count; i++)
        {
            PointOfInterest poi = new PointOfInterest
            {
                Name = $"Точка #{i}",
                Latitude = 55.70 + random.NextDouble() * 0.1,
                Longitude = 37.60 + random.NextDouble() * 0.1
            };

            _queue.Add(poi);

            Console.WriteLine($"[Producer] добавлена {poi.Name}");
        }

        // Сигнал: новых задач не будет.
        _queue.CompleteAdding();

        Console.WriteLine("[Producer] завершил работу (CompleteAdding).");
    }
}
