
using InteractiveMap.Console.Factories;

namespace InteractiveMap.Console.Bridge;

/// <summary>
/// Abstraction в паттерне Мост: высокоуровневая логика карты.
/// Содержит ссылку на Implementor (_renderer) — сам "мост".
/// </summary>
public abstract class Map
{
    // Мост.
    protected readonly IMapRenderer _renderer;
    protected readonly List<IPointOfInterest> _points = new();

    protected Map(IMapRenderer renderer)
    {
        _renderer = renderer;
    }

    public void AddPoint(IPointOfInterest poi)
    {
        _points.Add(poi);
    }

    /// <summary>
    /// Высокоуровневая операция: делегирует низкоуровневую отрисовку в Implementor.
    /// </summary>
    public abstract void Draw();
}
