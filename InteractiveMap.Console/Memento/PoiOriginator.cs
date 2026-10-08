namespace InteractiveMap.Console.Memento;

/// <summary>
/// Originator в паттерне Снимок: хранит редактируемое состояние
/// точки интереса, умеет создавать снимок и восстанавливаться из него.
/// </summary>
public sealed class PoiOriginator
{
    private string _name;
    private string? _category;
    private double? _rating;
    private string? _openingHours;

    public PoiOriginator(string name)
    {
        _name = name;
    }

    public string Name => _name;
    public string? Category => _category;
    public double? Rating => _rating;
    public string? OpeningHours => _openingHours;

    /// <summary>
    /// Изменяет состояние точки. Снимок предыдущего состояния
    /// делает клиент через Save() до изменения.
    /// </summary>
    public void Edit(string name, string? category, double? rating, string? openingHours)
    {
        _name = name;
        _category = category;
        _rating = rating;
        _openingHours = openingHours;
    }

    /// <summary>
    /// Создаёт снимок текущего состояния.
    /// </summary>
    public PoiMemento Save()
    {
        return new PoiMemento(_name, _category, _rating, _openingHours);
    }

    /// <summary>
    /// Восстанавливает состояние из снимка.
    /// </summary>
    public void Restore(PoiMemento memento)
    {
        _name = memento.Name;
        _category = memento.Category;
        _rating = memento.Rating;
        _openingHours = memento.OpeningHours;
    }

    public string Describe() =>
        $"\"{_name}\" | {_category ?? "—"} | *{(_rating.HasValue ? _rating.Value.ToString("0.0") : "—")} | {_openingHours ?? "—"}";
}