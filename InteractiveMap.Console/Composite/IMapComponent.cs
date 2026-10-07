namespace InteractiveMap.Console.Composite;

/// <summary>
/// Component в паттерне Компоновщик: общий интерфейс
/// для листа (точки) и группы (региона).
/// </summary>
public interface IMapComponent
{
    string Name { get; }
    string Describe();
    int Count();
}