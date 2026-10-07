using System.Text;

namespace InteractiveMap.Console.Composite;

/// <summary>
/// Composite в паттерне Компоновщик: регион карты,
/// содержит дочерние компоненты (точки или подрегионы).
/// </summary>
public sealed class MapRegionComposite : IMapComponent
{
    public string Name { get; }
    private readonly List<IMapComponent> _children = new();

    public MapRegionComposite(string name)
    {
        Name = name;
    }

    public void Add(IMapComponent child) => _children.Add(child);

    public bool Remove(IMapComponent child) => _children.Remove(child);

    public string Describe()
    {
        StringBuilder sb = new();
        sb.AppendLine($"[Регион] {Name} (элементов: {Count()})");

        foreach (IMapComponent child in _children)
        {
            sb.AppendLine(child.Describe());
        }

        return sb.ToString().TrimEnd();
    }

    public int Count()
    {
        int total = 0;

        foreach (IMapComponent child in _children)
        {
            total += child.Count();
        }

        return total;
    }
}