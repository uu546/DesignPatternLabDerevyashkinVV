namespace InteractiveMap.Console.Decorator;

/// <summary>
/// Decorator: добавляет рейтинг к выводу.
/// </summary>
public sealed class RatingDecorator : PoiFormatterDecorator
{
    public RatingDecorator(IPoiFormatter inner) : base(inner) { }

    public override string Format(string name, string category, double rating, string hours)
    {
        string baseResult = _inner.Format(name, category, rating, hours);

        return $"{baseResult} *{rating:0.0}";
    }
}
