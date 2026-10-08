
namespace InteractiveMap.Console.State;
/// <summary>
/// ConcreteState: черновик. Редактируется, не виден публично.
/// </summary>
public sealed class DraftState : IPoiState
{
    public string Name => "Draft";
    public bool CanEdit() => true;
    public bool IsVisible() => false;

    public void Publish(PoiContext context) => context.ChangeState(new PublishedState());
    public void Archive(PoiContext context) => context.ChangeState(new ArchivedState());
    public void ResetToDraft(PoiContext context) { /* уже в Draft */ }
}