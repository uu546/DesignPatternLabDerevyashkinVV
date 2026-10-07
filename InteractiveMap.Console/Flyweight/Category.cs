namespace InteractiveMap.Console.Flyweight;

/// <summary>
/// Flyweight (ConcreteFlyweight) в паттерне Приспособленец.
/// Хранит ВНУТРЕННЕЕ (intrinsic) состояние — разделяемое между
/// всеми точками одной категории.
/// </summary>
public sealed class Category
{
    public string Key { get; }
    public string DisplayName { get; }
    public string Icon { get; }

    public Category(string key, string displayName, string icon)
    {
        Key = key;
        DisplayName = displayName;
        Icon = icon;
    }

    public override string ToString() => $"{Icon} {DisplayName}";
}