namespace InteractiveMap.Console.Decorator;

/// <summary>
/// Decorator: абстрактная обёртка, хранит ссылку на inner
/// и реализует тот же интерфейс IPoiFormatter.
/// </summary>
public abstract class PoiFormatterDecorator : IPoiFormatter
{
    protected readonly IPoiFormatter _inner;

    protected PoiFormatterDecorator(IPoiFormatter inner)
    {
        _inner = inner;
    }

    public abstract string Format(string name, string category, double rating, string hours);
}