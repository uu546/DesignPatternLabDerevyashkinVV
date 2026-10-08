using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Iterators;

/// <summary>
/// ConcreteIterator: обходит коллекцию точек,
/// пропуская элементы, не подходящие по категории.
/// </summary>
public sealed class CategoryFilterIterator : IPoiIterator
{
    private readonly IReadOnlyList<PointOfInterest> _items;
    private readonly string? _category;
    private int _position;

    public CategoryFilterIterator(IReadOnlyList<PointOfInterest> items, string? category)
    {
        _items = items;
        _category = category;
        _position = 0;
    }

    public bool HasNext()
    {
        int scan = _position;

        while (scan < _items.Count)
        {
            if (Matches(_items[scan]))
            {
                return true;
            }

            scan++;
        }

        return false;
    }

    public PointOfInterest Next()
    {
        while (_position < _items.Count)
        {
            PointOfInterest current = _items[_position++];

            if (Matches(current))
            {
                return current;
            }
        }

        throw new InvalidOperationException("В коллекции больше нет элементов, удовлетворяющих фильтру.");
    }

    private bool Matches(PointOfInterest poi)
    {
        if (_category is null)
        {
            return true;
        }

        return string.Equals(poi.Category, _category, StringComparison.OrdinalIgnoreCase);
    }
}