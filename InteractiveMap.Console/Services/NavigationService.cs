using InteractiveMap.Console.Models;
using InteractiveMap.Console.Strategy;

namespace InteractiveMap.Console.Services;

/// <summary>
/// Context в паттерне Стратегия: хранит ссылку на текущую стратегию
/// расчёта расстояния и делегирует ей работу.
/// </summary>
public sealed class NavigationService
{
    private readonly MapService _service;
    private IDistanceStrategy _strategy;

    public NavigationService(MapService service, IDistanceStrategy strategy)
    {
        _service = service;
        _strategy = strategy;
    }

    public string CurrentStrategy => _strategy.Name;

    public void SetStrategy(IDistanceStrategy strategy)
    {
        _strategy = strategy;
    }

    public double Distance(int id1, int id2)
    {
        PointOfInterest a = _service.All.FirstOrDefault(p => p.Id == id1)
            ?? throw new ArgumentException($"Точка с Id={id1} не найдена.");

        PointOfInterest b = _service.All.FirstOrDefault(p => p.Id == id2)
            ?? throw new ArgumentException($"Точка с Id={id2} не найдена.");

        return _strategy.Calculate(
            a.Coordinate.Latitude, a.Coordinate.Longitude,
            b.Coordinate.Latitude, b.Coordinate.Longitude);
    }
}