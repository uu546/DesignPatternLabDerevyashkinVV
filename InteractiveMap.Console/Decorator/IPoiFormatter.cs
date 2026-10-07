namespace InteractiveMap.Console.Decorator;

/// <summary>
/// Декоратор: общий интерфейс
/// для базового форматтера и декораторов.
/// </summary>
public interface IPoiFormatter
{
    string Format(string name, string category, double rating, string hours);
}