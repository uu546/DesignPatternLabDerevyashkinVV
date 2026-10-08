using InteractiveMap.Console.Models;
using System.Text;

namespace InteractiveMap.Console.Export;

/// <summary>
/// AbstractClass в паттерне Шаблонный метод: определяет
/// СКЕЛЕТ алгоритма экспорта карты и оставляет
/// конкретные шаги (WriteHeader/WritePoint/WriteFooter) подклассам.
/// </summary>
public abstract class MapExporter
{
    /// <summary>
    /// Шаблонный метод: финальный скелет алгоритма.
    /// Порядок шагов зафиксирован и не переопределяется.
    /// </summary>
    public string Export(IReadOnlyList<PointOfInterest> pois)
    {
        StringBuilder sb = new();

        List<PointOfInterest> sorted = Sort(pois);

        sb.AppendLine(WriteHeader());

        foreach (PointOfInterest poi in sorted)
        {
            sb.AppendLine(WritePoint(poi));
        }

        sb.AppendLine(WriteFooter(sorted.Count));

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Общий шаг для всех форматов: сортировка по имени.
    /// Не переопределяется — часть скелета.
    /// </summary>
    protected List<PointOfInterest> Sort(IReadOnlyList<PointOfInterest> pois)
    {
        return pois.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }

    // --- Изменяемые шаги (переопределяются в подклассах) ---
    protected abstract string WriteHeader();
    protected abstract string WritePoint(PointOfInterest poi);
    protected abstract string WriteFooter(int count);
}