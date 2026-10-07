using InteractiveMap.Console.Adapters;
using InteractiveMap.Console.Bridge;
using InteractiveMap.Console.Factories;
using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

namespace InteractiveMap.Console.Facade;

/// <summary>
/// Facade в паттерне Фасад: единая точка входа в подсистему
/// «Интерактивная карта». Скрывает фабрики, адаптер, реестр, рендереры.
/// </summary>
public sealed class MapFacade
{
    private readonly MapService _service;

    public MapFacade()
    {
        _service = MapService.Instance;
    }

    /// <summary>
    /// Высокоуровневая операция: создать туристическую точку
    /// и зарегистрировать её в реестре.
    /// </summary>
    public void AddTouristPoi(string name, double latitude, double longitude)
    {
        IMapFactory factory = new TouristMapFactory();
        IPointOfInterest poi = factory.CreatePoi(name, new Coordinate(latitude, longitude));
        PointOfInterest adapted = new PointOfInterestAdapter(poi).ToPointOfInterest();
        _service.AddPoi(adapted);
    }

    /// <summary>
    /// Высокоуровневая операция: создать гастрономическую точку
    /// и зарегистрировать её в реестре.
    /// </summary>
    public void AddFoodPoi(string name, double latitude, double longitude)
    {
        IMapFactory factory = new FoodMapFactory();
        IPointOfInterest poi = factory.CreatePoi(name, new Coordinate(latitude, longitude));
        PointOfInterest adapted = new PointOfInterestAdapter(poi).ToPointOfInterest();
        _service.AddPoi(adapted);
    }

    /// <summary>
    /// Высокоуровневая операция: отрисовать карту туристическим
    /// рендерером в консоль.
    /// </summary>
    public void RenderTouristConsole()
    {
        Map map = new TouristicMap(new ConsoleRenderer());

        foreach (PointOfInterest poi in _service.All)
        {
            map.AddPoint(new PoiLeafAdapter(poi));
        }

        map.Draw();
    }

    /// <summary>
    /// Высокоуровневая операция: отрисовать карту графическим
    /// рендерером.
    /// </summary>
    public void RenderFoodGraphics()
    {
        Map map = new FoodMap(new GraphicsWindowRenderer());

        foreach (PointOfInterest poi in _service.All)
        {
            map.AddPoint(new PoiLeafAdapter(poi));
        }

        map.Draw();
    }

    public int TotalCount() => _service.All.Count;
}