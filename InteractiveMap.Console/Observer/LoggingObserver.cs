using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Observer;

/// <summary>
/// ConcreteObserver: логирует события реестра.
/// </summary>
public sealed class LoggingObserver : IMapObserver
{
    public void OnPoiAdded(PointOfInterest poi)
    {
        System.Console.WriteLine($"[Log] Добавлена точка #{poi.Id} \"{poi.Name}\"");
    }

    public void OnPoiRemoved(int id)
    {
        System.Console.WriteLine($"[Log] Удалена точка #{id}");
    }
}