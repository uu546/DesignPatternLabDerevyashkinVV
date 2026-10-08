namespace InteractiveMap.Console.State;

/// <summary>
/// State в паттерне Состояние: общий интерфейс поведения точки
/// в зависимости от её жизненного цикла.
/// </summary>
public interface IPoiState
{
    string Name { get; }
    bool CanEdit();
    bool IsVisible();
    void Publish(PoiContext context);
    void Archive(PoiContext context);
    void ResetToDraft(PoiContext context);
}