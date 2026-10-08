namespace InteractiveMap.Console.Strategy;

/// <summary>
/// ConcreteStrategy: расстояние по формуле гаверсинуса
/// (кратчайшее расстояние по поверхности Земли).
/// </summary>
public sealed class HaversineStrategy : IDistanceStrategy
{
    public string Name => "Haversine";

    public double Calculate(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371.0;

        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);
        double radLat1 = ToRadians(lat1);
        double radLat2 = ToRadians(lat2);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                 + Math.Cos(radLat1) * Math.Cos(radLat2)
                 * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        return 2 * R * Math.Asin(Math.Sqrt(a));
    }

    private static double ToRadians(double deg) => deg * Math.PI / 180.0;
}