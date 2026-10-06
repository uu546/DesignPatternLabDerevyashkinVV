using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Factories;

/// <summary>
/// Конкретный продукт: Туристическая точка
/// </summary>
public sealed class TouristPointOfInterest : IPointOfInterest
{
    public string Name { get; }
    public Coordinate Coordinate { get; }
    public string Category => "Достопримечательность";
    public double Rating => 4.5;
    public string OpeningHours => "09:00–18:00";

    public TouristPointOfInterest(string name, Coordinate coordinate)
    {
        Name = name;
        Coordinate = coordinate;
    }
}

/// <summary>
/// Конкретный продукт: Историческое описание
/// </summary>
public sealed class TouristDescription : IPoiDescription
{
    public string Content { get; }

    public TouristDescription(string name)
    {
        Content = $"Историческая справка: {name} — объект культурного наследия.";
    }
}