using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Export;

public sealed class CsvExporter : MapExporter
{
    protected override string WriteHeader() => "Name,Category,Latitude,Longitude";

    protected override string WritePoint(PointOfInterest poi)
        => $"\"{poi.Name}\",\"{poi.Category ?? ""}\",{poi.Coordinate.Latitude},{poi.Coordinate.Longitude}";

    protected override string WriteFooter(int count) => $"# rows: {count}";
}
