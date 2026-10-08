using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Iterators;

/// <summary>
/// Iterator в паттерне Итератор: интерфейс обхода коллекции точек.
/// </summary>
public interface IPoiIterator
{
    bool HasNext();
    PointOfInterest Next();
}