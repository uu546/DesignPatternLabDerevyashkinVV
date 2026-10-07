using InteractiveMap.Console.Abstractions;

namespace InteractiveMap.Console.Proxies;

/// <summary>
/// Proxy в паттерне Заместитель (Caching Proxy).
/// Реализует тот же интерфейс, что RealSubject, хранит ссылку на него
/// и кэширует результат по паре (id1, id2) с учётом симметрии.
/// </summary>
public sealed class CachingDistanceProxy : IDistanceCalculator
{
    private readonly IDistanceCalculator _real;
    private readonly Dictionary<(int, int), double> _cache = new();

    private int _hits;
    private int _misses;

    public CachingDistanceProxy(IDistanceCalculator real)
    {
        _real = real;
    }

    public int Hits => _hits;
    public int Misses => _misses;
    public int CacheSize => _cache.Count;

    public double GetDistance(int id1, int id2)
    {
        // Симметрия: расстояние (a, b) == (b, a). Нормализуем ключ.
        (int, int) key = id1 <= id2 ? (id1, id2) : (id2, id1);

        if (_cache.TryGetValue(key, out double cached))
        {
            _hits++;
            return cached;
        }

        _misses++;
        double result = _real.GetDistance(id1, id2);
        _cache[key] = result;

        return result;
    }
}