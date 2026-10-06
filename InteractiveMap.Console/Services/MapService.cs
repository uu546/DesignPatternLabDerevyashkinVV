using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Services;

public sealed class MapService
{
    private readonly List<PointOfInterest> _pois = new();

    private int _nextId = 1;

    public IReadOnlyList<PointOfInterest> All => _pois;

    private static readonly Lazy<MapService> _instance =
        new(() => new MapService(), isThreadSafe: true);

    public static MapService Instance => _instance.Value;

    private MapService() { }

    public PointOfInterest AddPoi(PointOfInterest poi)
    {
        if (poi is null)
            throw new ArgumentException("Переданная точка по интересам не может быть пустой", nameof(poi));
      
        PointOfInterest result = new()
        {
            Id = _nextId++,
            Name = poi.Name,
            Coordinate = poi.Coordinate,
            Description = poi.Description,
            Category = poi.Category,
            Rating = poi.Rating,
            OpeningHours = poi.OpeningHours
        };

        _pois.Add(result);

        return result;
    }

    public bool RemovePoi(int id) => _pois.RemoveAll(p => p.Id == id) > 0;

    public PointOfInterest? FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        PointOfInterest? result = _pois.FirstOrDefault(p =>
            p.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));

        System.Console.WriteLine($"Результат поиска: {result}");

        return result;
    }

    public double GetDistance(int id1, int id2)
    {
        PointOfInterest a = _pois.FirstOrDefault(p => p.Id == id1)
            ?? throw new ArgumentException($"Точка с Id={id1} не найдена.", nameof(id1));

        PointOfInterest b = _pois.FirstOrDefault(p => p.Id == id2)
            ?? throw new ArgumentException($"Точка с Id={id2} не найдена.", nameof(id2));

        // Радиус земли в км.
        const double R = 6371.0;

        // Дельта между точками по широте.
        double dLat = ToRadians(b.Coordinate.Latitude - a.Coordinate.Latitude);

        // Дельта между точками по долготе.
        double dLon = ToRadians(b.Coordinate.Longitude - a.Coordinate.Longitude);

        // Перевод широт в радианы.
        double lat1 = ToRadians(a.Coordinate.Latitude);
        double lat2 = ToRadians(b.Coordinate.Latitude);

        // Вычисление кратчайшего расстояния между точек (используется формула гаверсинуса).
        double dlatSin = Math.Sin(dLat / 2) * Math.Sin(dLat / 2);
        double latCos = Math.Cos(lat1) * Math.Cos(lat2);
        double dLonSin = Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double h = dlatSin + (latCos * dLonSin);

        // Вычисляем расстояние в киллометры.
        double result = 2 * R * Math.Asin(Math.Sqrt(h));

        return result;
    }

    /// <summary>
    /// Метод переводит градусы в радианы, для работы с синусом.
    /// </summary>
    /// <param name="deg">Градусы.</param>
    /// <returns>Приведенные градусы.</returns>
    private double ToRadians(double deg) => deg * Math.PI / 180.0;
}