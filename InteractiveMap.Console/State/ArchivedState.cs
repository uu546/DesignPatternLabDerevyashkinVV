namespace InteractiveMap.Console.State;

/// <summary>
/// ConcreteState: архив. Не редактируется, не отображается.
/// </summary>
public sealed class ArchivedState : IPoiState
{
    public string Name => "Archived";
    public bool CanEdit() => false;
    public bool IsVisible() => false;

    public void Publish(PoiContext context) => context.ChangeState(new PublishedState());
    public void Archive(PoiContext context) {}
    public void ResetToDraft(PoiContext context) => context.ChangeState(new DraftState());
}