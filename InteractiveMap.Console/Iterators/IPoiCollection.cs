namespace InteractiveMap.Console.Iterators;

/// <summary>
/// Aggregate в паттерне Итератор: фабрика итераторов.
/// </summary>
public interface IPoiCollection
{
    IPoiIterator CreateIterator();
    IPoiIterator CreateIterator(string category);
}