namespace InteractiveMap.Console.Mediation;

/// <summary>
/// Mediator в паттерне Посредник: интерфейс взаимодействия с коллегами.
/// </summary>
public interface IMapMediator
{
    bool AddPoi(string name, double latitude, double longitude, string category, double rating);
    bool RemovePoi(int id);
    void RenderAll();
    void Notify(IMapColleague sender, string evt);
}