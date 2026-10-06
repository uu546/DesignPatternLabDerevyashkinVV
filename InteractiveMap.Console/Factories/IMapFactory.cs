using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Factories;

/// <summary>
/// Интерфейс абстрактной фабрики.
/// </summary>
public interface IMapFactory
{
    /// <summary>
    /// Фабричный метод.
    /// </summary>
    IPointOfInterest CreatePoi(string name, Coordinate coordinate);

    /// <summary>
    /// Фабричный метод.
    /// </summary>
    IPoiDescription CreateDescription(string name);
}