using Lr15;
using System.Diagnostics;

Console.WriteLine("===== Producer/Consumer =====\n");

Stopwatch sw = Stopwatch.StartNew();

Pipeline pipeline = new Pipeline();
pipeline.Run(totalPois: 10, consumerCount: 3);

sw.Stop();
Console.WriteLine($"Общее время: {sw.ElapsedMilliseconds} мс");