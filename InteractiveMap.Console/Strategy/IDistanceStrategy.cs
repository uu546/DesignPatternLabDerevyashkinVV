
namespace InteractiveMap.Console.Strategy;

/// <summary>
/// Strategy в паттерне Стратегия: общий интерфейс алгоритмов
/// расчёта расстояния между двумя точками.
/// </summary>
public interface IDistanceStrategy
{
    string Name { get; }
    double Calculate(double lat1, double lon1, double lat2, double lon2);
}