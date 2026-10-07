namespace InteractiveMap.Console.Bridge;

/// <summary>
/// ConcreteImplementor: отрисовка в консоль.
/// </summary>
public sealed class ConsoleRenderer : IMapRenderer
{
    public void RenderBackground()
    {
        System.Console.WriteLine("[Консоль]: Отрисовка сетки координат...");
    }

    public void RenderPoint(string name, string category)
    {
        System.Console.WriteLine($"[Консоль]: точка {name} ({category})");
    }
}