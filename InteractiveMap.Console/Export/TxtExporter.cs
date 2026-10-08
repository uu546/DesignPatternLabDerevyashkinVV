using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Export;

public sealed class TxtExporter : MapExporter
{
    protected override string WriteHeader() => "=== КАРТА (TXT) ===";

    protected override string WritePoint(PointOfInterest poi)
        => $"* {poi.Name} [{poi.Category ?? "—"}] @ ({poi.Coordinate.Latitude}, {poi.Coordinate.Longitude})";

    protected override string WriteFooter(int count) => $"--- Всего точек: {count} ---";
}