using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Factories;

/// <summary>
/// Конкретная кулинарная фабрика
/// </summary>
public sealed class FoodMapFactory : IMapFactory
{
    public IPointOfInterest CreatePoi(string name, Coordinate coordinate)
    {
        return new FoodPointOfInterest(name, coordinate);
    }

    public IPoiDescription CreateDescription(string name)
    {
        return new FoodDescription(name);
    }
}