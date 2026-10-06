using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Factories;

/// <summary>
/// Конкретный продукт: Кулинарная точка
/// </summary>
public sealed class FoodPointOfInterest : IPointOfInterest
{
    public string Name { get; }
    public Coordinate Coordinate { get; }
    public string Category => "Заведение";
    public double Rating => 4.2;
    public string OpeningHours => "10:00–23:00";

    public FoodPointOfInterest(string name, Coordinate coordinate)
    {
        Name = name;
        Coordinate = coordinate;
    }
}

public sealed class FoodDescription : IPoiDescription
{
    public string Content { get; }

    public FoodDescription(string name)
    {
        Content = $"Кулинарное описание: {name} — популярное место с авторской кухней.";
    }
}