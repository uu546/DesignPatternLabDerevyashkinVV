namespace InteractiveMap.Console.Decorator;

/// <summary>
/// ConcreteDecorator: добавляет категорию к выводу.
/// </summary>
public sealed class CategoryDecorator : PoiFormatterDecorator
{
    public CategoryDecorator(IPoiFormatter inner) : base(inner) { }

    public override string Format(string name, string category, double rating, string hours)
    {
        string baseResult = _inner.Format(name, category, rating, hours);
        return $"{baseResult} ({category})";
    }
}