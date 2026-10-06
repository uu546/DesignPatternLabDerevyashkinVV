
namespace InteractiveMap.Console.Models;

public sealed class PointOfInterest
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public Coordinate Coordinate { get; set; } = null!;

    public override string ToString() => $"#{Id} | {Name} | {Coordinate}";
}