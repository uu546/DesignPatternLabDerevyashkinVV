namespace InteractiveMap.Console.Bridge;

/// <summary>
/// ConcreteImplementor: симуляция графического окна.
/// </summary>
public sealed class GraphicsWindowRenderer : IMapRenderer
{
    public void RenderBackground()
    {
        System.Console.WriteLine("[Окно]: Загрузка текстур и спутниковых снимков...");
    }

    public void RenderPoint(string name, string category)
    {
        System.Console.WriteLine($"[Окно]: [Рисуем иконку для {category}] -> {name}");
    }
}
