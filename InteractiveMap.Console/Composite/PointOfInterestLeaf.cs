namespace InteractiveMap.Console.Composite;

/// <summary>
/// Leaf в паттерне Компоновщик: конечная точка интереса.
/// </summary>
public sealed class PointOfInterestLeaf : IMapComponent
{
    public string Name { get; }
    public string Category { get; }
    public string Coordinate { get; }

    public PointOfInterestLeaf(string name, string category, string coordinate)
    {
        Name = name;
        Category = category;
        Coordinate = coordinate;
    }

    public string Describe() => $"  • {Name} [{Category}] @ {Coordinate}";

    public int Count() => 1;
}