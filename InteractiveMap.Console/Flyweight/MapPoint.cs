namespace InteractiveMap.Console.Flyweight;

/// <summary>
/// Client в паттерне Приспособленец.
/// Хранит внешнее состояние — уникальное для каждой точки,
/// и ссылается на разделяемый приспособленец Category .
/// </summary>
public sealed class MapPoint
{
    private readonly Category _category;

    public string Name { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public MapPoint(string name, double latitude, double longitude, Category category)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        _category = category;
    }

    public string Describe() =>
        $"{_category} {Name} @ ({Latitude}, {Longitude})";
}