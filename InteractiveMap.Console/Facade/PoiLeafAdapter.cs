using InteractiveMap.Console.Factories;
using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Facade;

/// <summary>
/// Вспомогательный адаптер: PointOfInterest -> IPointOfInterest,
/// чтобы использовать существующий Bridge (Lab3) поверх реестра.
/// </summary>
internal sealed class PoiLeafAdapter : IPointOfInterest
{
    private readonly PointOfInterest _source;

    public PoiLeafAdapter(PointOfInterest source)
    {
        _source = source;
    }

    public string Name => _source.Name;
    public Coordinate Coordinate => _source.Coordinate;
    public string Category => _source.Category ?? "—";
    public double Rating => _source.Rating ?? 0.0;
    public string OpeningHours => _source.OpeningHours ?? "—";
}