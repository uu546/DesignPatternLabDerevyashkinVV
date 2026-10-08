using InteractiveMap.Console.Bridge;
using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;
using InteractiveMap.Console.Validation;

namespace InteractiveMap.Console.Mediation;

/// <summary>
/// ConcreteMediator: централизует взаимодействие между
/// реестром (MapService), валидатором (PoiValidationPipeline)
/// и рендерером (IMapRenderer).
/// </summary>
public sealed class MapMediator : IMapMediator
{
    private readonly MapService _service;
    private readonly PoiValidationPipeline _pipeline;
    private readonly IMapRenderer _renderer;

    public MapMediator(IMapRenderer renderer)
    {
        _service = MapService.Instance;
        _pipeline = new PoiValidationPipeline();
        _renderer = renderer;
    }

    public bool AddPoi(string name, double latitude, double longitude, string category, double rating)
    {
        PointOfInterest poi = new()
        {
            Name = name,
            Coordinate = new Coordinate(latitude, longitude),
            Category = category,
            Rating = rating
        };

        // 1. Валидация через цепочку обязанностей (Lab8).
        ValidationResult validation = _pipeline.Validate(poi);

        if (!validation.IsValid)
        {
            System.Console.WriteLine($"[Mediator] Отклонено: {validation.ErrorMessage}");
            return false;
        }

        // 2. Регистрация в реестре.
        _service.AddPoi(poi);
        return true;
    }

    public bool RemovePoi(int id)
    {
        return _service.RemovePoi(id);
    }

    public void RenderAll()
    {
        _renderer.RenderBackground();

        foreach (PointOfInterest poi in _service.All)
        {
            _renderer.RenderPoint(poi.Name, poi.Category ?? "—");
        }
    }

    public void Notify(IMapColleague sender, string evt)
    {
        System.Console.WriteLine($"[Mediator] Событие \"{evt}\" от коллеги {sender.GetType().Name}");
    }
}