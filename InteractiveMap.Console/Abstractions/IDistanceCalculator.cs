namespace InteractiveMap.Console.Abstractions;

/// <summary>
/// Subject в паттерне Заместитель: общий интерфейс
/// для реального калькулятора и его прокси.
/// </summary>
public interface IDistanceCalculator
{
    double GetDistance(int id1, int id2);
}