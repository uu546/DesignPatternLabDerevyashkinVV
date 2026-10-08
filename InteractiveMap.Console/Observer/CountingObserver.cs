using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Observer;

/// <summary>
/// ConcreteObserver: ведёт счётчики добавлений и удалений.
/// </summary>
public sealed class CountingObserver : IMapObserver
{
    public int Added { get; private set; }
    public int Removed { get; private set; }

    public void OnPoiAdded(PointOfInterest poi)
    {
        Added++;
    }

    public void OnPoiRemoved(int id)
    {
        Removed++;
    }
}