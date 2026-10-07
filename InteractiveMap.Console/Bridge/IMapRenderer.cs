
namespace InteractiveMap.Console.Bridge;

/// <summary>
/// Implementor в паттерне Мост: низкоуровневая отрисовка.
/// </summary>
public interface IMapRenderer
{
    /// <summary>
    /// Метод отрисовывает вид отображения карты (консоль, графика).
    /// </summary>
    void RenderBackground();

    /// <summary>
    /// Метод отображает точку.
    /// </summary>
    /// <param name="name">Название точки.</param>
    /// <param name="category">Категория точки.</param>
    void RenderPoint(string name, string category);
}