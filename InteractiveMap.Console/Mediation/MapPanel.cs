namespace InteractiveMap.Console.Mediation;

/// <summary>
/// ConcreteColleague: панель управления картой.
/// Общается только с посредником, не знает
/// о других коллегах и подсистемах.
/// </summary>
public sealed class MapPanel : IMapColleague
{
    private IMapMediator? _mediator;

    public void SetMediator(IMapMediator mediator)
    {
        _mediator = mediator;
    }

    public void RequestAdd(string name, double latitude, double longitude, string category, double rating)
    {
        if (_mediator is null)
        {
            throw new InvalidOperationException("Посредник не установлен.");
        }

        bool ok = _mediator.AddPoi(name, latitude, longitude, category, rating);

        _mediator.Notify(this, ok ? "PoiAdded" : "PoiRejected");
    }

    public void RequestRender()
    {
        _mediator?.RenderAll();
        _mediator?.Notify(this, "RenderRequested");
    }
}
