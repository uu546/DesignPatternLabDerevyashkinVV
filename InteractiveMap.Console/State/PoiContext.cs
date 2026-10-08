namespace InteractiveMap.Console.State;

/// <summary>
/// Context в паттерне Состояние: хранит текущее состояние
/// и делегирует ему операции.
/// </summary>
public sealed class PoiContext
{
    private string _name;
    private IPoiState _state;

    public PoiContext(string name)
    {
        _name = name;
        _state = new DraftState();
    }

    public string Name => _name;
    public IPoiState State => _state;

    public void ChangeState(IPoiState newState)
    {
        System.Console.WriteLine($"[State] {_state.Name} -> {newState.Name}");
        _state = newState;
    }

    public void Edit(string newName)
    {
        if (!_state.CanEdit())
        {
            System.Console.WriteLine($"[State] Редактирование запрещено в состоянии {_state.Name}");
            return;
        }

        _name = newName;
        System.Console.WriteLine($"[State] Имя изменено на \"{_name}\"");
    }

    public void Publish() => _state.Publish(this);
    public void Archive() => _state.Archive(this);
    public void ResetToDraft() => _state.ResetToDraft(this);

    public string Describe() =>
        $"\"{_name}\" | state: {_state.Name} | editable: {_state.CanEdit()} | visible: {_state.IsVisible()}";
}