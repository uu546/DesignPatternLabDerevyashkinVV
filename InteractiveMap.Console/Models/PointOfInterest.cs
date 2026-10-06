namespace InteractiveMap.Console.Models;

public sealed class PointOfInterest
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public Coordinate Coordinate { get; set; } = null!;
    public string? Description { get; init; }
    public string? Category { get; init; }
    public double? Rating { get; init; }
    public string? OpeningHours { get; init; }

    public override string ToString()
    {
        string category = Category?.ToString() ?? "—";
        string rating = Rating.HasValue
            ? Rating.Value.ToString("0.0")
            : "—";

        return $"#{Id} | {Name} | {Coordinate} | {category} | *{rating}";
    }
}