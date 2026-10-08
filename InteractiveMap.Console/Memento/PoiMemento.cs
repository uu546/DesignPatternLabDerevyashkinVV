
namespace InteractiveMap.Console.Memento;

/// <summary>
/// Memento в паттерне Снимок: хранит внутреннее состояние
/// Originator. Снаружи — read-only.
/// </summary>
public sealed class PoiMemento
{
    public string Name { get; }
    public string? Category { get; }
    public double? Rating { get; }
    public string? OpeningHours { get; }

    internal PoiMemento(string name, string? category, double? rating, string? openingHours)
    {
        Name = name;
        Category = category;
        Rating = rating;
        OpeningHours = openingHours;
    }
}