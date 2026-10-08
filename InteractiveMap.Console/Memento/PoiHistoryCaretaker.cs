namespace InteractiveMap.Console.Memento;

/// <summary>
/// Caretaker в паттерне Снимок: хранит снимки,
/// не читая их содержимого.
/// </summary>
public sealed class PoiHistoryCaretaker
{
    private readonly Stack<PoiMemento> _history = new();

    public int Count => _history.Count;

    public void Save(PoiMemento memento)
    {
        _history.Push(memento);
    }

    /// <summary>
    /// Возвращает последний снимок (LIFO), удаляя его из истории.
    /// </summary>
    public PoiMemento? Undo()
    {
        if (_history.Count == 0)
        {
            return null;
        }

        return _history.Pop();
    }
}