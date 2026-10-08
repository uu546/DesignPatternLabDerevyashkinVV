namespace InteractiveMap.Console.Mediation;

/// <summary>
/// Colleague в паттерне Посредник: знает только о посреднике.
/// </summary>
public interface IMapColleague
{
    void SetMediator(IMapMediator mediator);
}