using InteractiveMap.Console.Factories;

namespace InteractiveMap.Console.Bridge;

/// <summary>
/// RefinedAbstraction: туристическая карта.
/// </summary>
public sealed class TouristicMap : Map
{
    public TouristicMap(IMapRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        System.Console.WriteLine("=== ЗАПУСК ТУРИСТИЧЕСКОЙ КАРТЫ ===");
        _renderer.RenderBackground();

        foreach (IPointOfInterest point in _points)
        {
            _renderer.RenderPoint(point.Name, point.Category);
        }
    }
}
