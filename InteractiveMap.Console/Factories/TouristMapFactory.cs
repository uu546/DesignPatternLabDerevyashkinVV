using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Factories;

/// <summary>
/// Конкретная туристическая фабрика
/// </summary>
public sealed class TouristMapFactory : IMapFactory
{
    public IPointOfInterest CreatePoi(string name, Coordinate coordinate)
    {
        return new TouristPointOfInterest(name, coordinate);
    }

    public IPoiDescription CreateDescription(string name)
    {
        return new TouristDescription(name);
    }
}