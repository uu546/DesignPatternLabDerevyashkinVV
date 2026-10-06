namespace InteractiveMap.Console.Models;

public sealed class Coordinate
{
    public double Latitude { get; }
    public double Longitude { get; }

    public Coordinate(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), "Широта должна быть в диапазоне [-90; 90].");
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), "Долгота должна быть в диапазоне [-180; 180].");
        }

        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString()
    {
        return $"Ширина: {Latitude} | Высота: {Longitude}";
    }
}