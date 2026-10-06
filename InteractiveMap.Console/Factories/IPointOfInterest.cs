using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Factories;

public interface IPointOfInterest
{
    string Name { get; }
    Coordinate Coordinate { get; }
    string Category { get; }
    double Rating { get; }
    string OpeningHours { get; }
}

