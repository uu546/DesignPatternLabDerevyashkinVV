namespace InteractiveMap.Console.Commands;

/// <summary>
/// Command в паттерне Команда: общий интерфейс
/// с поддержкой выполнения и отмены.
/// </summary>
public interface ICommand
{
    void Execute();
    void Undo();
}