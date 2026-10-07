
using InteractiveMap.Console.Factories;

namespace InteractiveMap.Console.Bridge;

/// <summary>
/// RefinedAbstraction: гастрономическая карта.
/// </summary>
public sealed class FoodMap : Map
{
    public FoodMap(IMapRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        System.Console.WriteLine("=== ЗАПУСК КУЛИНАРНОГО ГИДА ===");
        _renderer.RenderBackground();
        System.Console.WriteLine("--> Рекомендуем посетить сегодня:");

        foreach (IPointOfInterest point in _points)
        {
            _renderer.RenderPoint(point.Name, point.Category);
        }
    }
}
