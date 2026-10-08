using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

namespace InteractiveMap.Console.Commands;

/// <summary>
/// ConcreteCommand: добавление точки в реестр.
/// </summary>
public sealed class AddPoiCommand : ICommand
{
    private readonly MapService _receiver;
    private readonly PointOfInterest _poi;
    private int _assignedId = -1;

    public AddPoiCommand(MapService receiver, PointOfInterest poi)
    {
        _receiver = receiver;
        _poi = poi;
    }

    public void Execute()
    {
        PointOfInterest stored = _receiver.AddPoi(_poi);

        _assignedId = stored.Id;
    }

    public void Undo()
    {
        if (_assignedId > 0)
        {
            _receiver.RemovePoi(_assignedId);
        }
    }
}