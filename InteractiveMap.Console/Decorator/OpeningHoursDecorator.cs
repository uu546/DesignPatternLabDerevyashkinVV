namespace InteractiveMap.Console.Decorator;

/// <summary>
/// Decorator: добавляет часы работы к выводу.
/// </summary>
public sealed class OpeningHoursDecorator : PoiFormatterDecorator
{
    public OpeningHoursDecorator(IPoiFormatter inner) : base(inner) { }

    public override string Format(string name, string category, double rating, string hours)
    {
        string baseResult = _inner.Format(name, category, rating, hours);
        return $"{baseResult} [{hours}]";
    }
}