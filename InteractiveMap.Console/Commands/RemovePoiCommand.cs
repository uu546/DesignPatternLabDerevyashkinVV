using InteractiveMap.Console.Models;
using InteractiveMap.Console.Services;

namespace InteractiveMap.Console.Commands;

/// <summary>
/// ConcreteCommand: удаление точки из реестра с возможностью отмены.
/// </summary>
public sealed class RemovePoiCommand : ICommand
{
    private readonly MapService _receiver;
    private readonly int _poiId;
    private PointOfInterest? _removed;

    public RemovePoiCommand(MapService receiver, int poiId)
    {
        _receiver = receiver;
        _poiId = poiId;
    }

    public void Execute()
    {
        _removed = _receiver.All.FirstOrDefault(p => p.Id == _poiId);

        if (_removed is not null)
        {
            _receiver.RemovePoi(_poiId);
        }
    }

    public void Undo()
    {
        if (_removed is not null)
        {
            _receiver.AddPoi(_removed);
            _removed = null;
        }
    }
}