using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Export;

public sealed class JsonExporter : MapExporter
{
    protected override string WriteHeader() => "[";

    protected override string WritePoint(PointOfInterest poi)
        => $"  {{ \"name\": \"{poi.Name}\", \"category\": \"{poi.Category ?? ""}\", \"lat\": " +
        $"{poi.Coordinate.Latitude}, \"lon\": {poi.Coordinate.Longitude} }},";

    protected override string WriteFooter(int count)
    {
        return $"]\n// total: {count}";
    }
}