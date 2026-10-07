namespace InteractiveMap.Console.Flyweight;

/// <summary>
/// FlyweightFactory: создаёт и кэширует приспособленцев Category.
/// Гарантирует, что для одного ключа существует ровно один объект.
/// </summary>
public static class CategoryFactory
{
    private static readonly Dictionary<string, Category> _cache = new();

    public static Category Get(string key)
    {
        if (_cache.TryGetValue(key, out Category? cached))
        {
            return cached;
        }

        Category created = key switch
        {
            "park" => new Category(key, "Парк", "park.img"),
            "cafe" => new Category(key, "Кафе", "cafe.img"),
            "museum" => new Category(key, "Музей", "museum.img"),
            "shop" => new Category(key, "Магазин", "shop.img"),
            _ => new Category(key, key, "default.img")
        };

        _cache[key] = created;

        return created;
    }

    public static int CacheSize => _cache.Count;
}