using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lr15;

/// <summary>
/// Coordinator в паттерне Producer/Consumer.
/// Создаёт очередь, запускает продюсера и N консьюмеров,
/// ждёт завершения всех.
/// </summary>
public sealed class Pipeline
{
    public void Run(int totalPois, int consumerCount)
    {
        BlockingCollection<PointOfInterest> queue = new BlockingCollection<PointOfInterest>();

        PoiProducer producer = new PoiProducer(queue);

        // Запускаем продюсера в отдельной задаче.
        Task producerTask = Task.Run(() => producer.Produce(totalPois));

        // Запускаем N консьюмеров, каждый — в своей задаче.
        Task[] consumerTasks = new Task[consumerCount];
        for (int i = 0; i < consumerCount; i++)
        {
            int consumerId = i + 1;
            PoiConsumer consumer = new PoiConsumer(queue, consumerId);
            consumerTasks[i] = Task.Run(() => consumer.Consume());
        }

        // Ждём завершения продюсера и всех консьюмеров.
        Task.WaitAll(consumerTasks);
        producerTask.Wait();

        Console.WriteLine("\n[Pipeline] Все задачи обработаны.");
    }
}