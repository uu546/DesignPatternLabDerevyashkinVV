using InteractiveMap.Console.Factories;
using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Adapters;

/// <summary>
/// Адаптирует IPointOfInterest (продукт фабрики)
/// под PointOfInterest (модель сервиса MapService).
/// </summary>
public sealed class PointOfInterestAdapter
{
    private readonly IPointOfInterest _source;

    public PointOfInterestAdapter(IPointOfInterest source)
    {
        _source = source;
    }

    public PointOfInterest ToPointOfInterest()
    {
        return new PointOfInterest
        {
            Name = _source.Name,
            Coordinate = _source.Coordinate,
            Category = _source.Category,
            Rating = _source.Rating,
            OpeningHours = _source.OpeningHours
        };
    }
}