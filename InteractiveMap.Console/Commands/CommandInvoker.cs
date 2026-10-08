namespace InteractiveMap.Console.Commands;

/// <summary>
/// Invoker в паттерне Команда: хранит историю команд,
/// выполняет их и умеет откатывать последнюю.
/// </summary>
public sealed class CommandInvoker
{
    private readonly List<ICommand> _history = new();

    public int HistorySize => _history.Count;

    public void Execute(ICommand command)
    {
        command.Execute();
        _history.Add(command);
    }

    public void UndoLast()
    {
        if (_history.Count == 0)
        {
            return;
        }

        ICommand last = _history[_history.Count - 1];

        last.Undo();

        _history.RemoveAt(_history.Count - 1);
    }
}