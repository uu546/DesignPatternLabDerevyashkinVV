namespace InteractiveMap.Console.State;

/// <summary>
/// ConcreteState: опубликована. Видна, редактирование ограничено.
/// </summary>
public sealed class PublishedState : IPoiState
{
    public string Name => "Published";
    public bool CanEdit() => false;
    public bool IsVisible() => true;

    public void Publish(PoiContext context) { /* уже опубликовано */ }
    public void Archive(PoiContext context) => context.ChangeState(new ArchivedState());
    public void ResetToDraft(PoiContext context) => context.ChangeState(new DraftState());
}