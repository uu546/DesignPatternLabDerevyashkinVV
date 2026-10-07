namespace InteractiveMap.Console.Decorator;

/// <summary>
/// Базовое форматирование — только имя.
/// </summary>
public sealed class BasePoiFormatter : IPoiFormatter
{
    public string Format(string name, string category, double rating, string hours)
    {
        return name;
    }
}
