using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Observer;

/// <summary>
/// Observer в паттерне Наблюдатель: подписчик на изменения реестра.
/// </summary>
public interface IMapObserver
{
    void OnPoiAdded(PointOfInterest poi);
    void OnPoiRemoved(int id);
}