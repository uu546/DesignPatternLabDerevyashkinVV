namespace InteractiveMap.Console.Strategy;

/// <summary>
/// ConcreteStrategy: манхэттенское расстояние.
/// Используется, когда важнее «разница по улицам», а не «по прямой».
/// </summary>
public sealed class ManhattanStrategy : IDistanceStrategy
{
    public string Name => "Manhattan";

    public double Calculate(double lat1, double lon1, double lat2, double lon2)
    {
        // Перевод градусов в километры.
        const double KmPerDegreeLat = 111.0;
        const double KmPerDegreeLonAtEquator = 111.0;

        double dLatKm = Math.Abs(lat2 - lat1) * KmPerDegreeLat;

        // Долгота сжимается к полюсам - приблизительно учитываем через cos(широты).
        double avgLat = (lat1 + lat2) / 2.0;
        double dLonKm = Math.Abs(lon2 - lon1) * KmPerDegreeLonAtEquator * Math.Cos(avgLat * Math.PI / 180.0);

        return dLatKm + dLonKm;
    }
}