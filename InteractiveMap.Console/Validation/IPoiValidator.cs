using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// Handler в паттерне Цепочка обязанностей:
/// обработчик одной проверки точки интереса.
/// </summary>
public interface IPoiValidator
{
    /// <summary>
    /// Устанавливает следующий обработчик в цепочке.
    /// Возвращает переданный next - для fluent-сборки цепочки.
    /// </summary>
    IPoiValidator SetNext(IPoiValidator next);

    /// <summary>
    /// Проверяет точку. Если валидатор считает проверку своей —
    /// возвращает результат. Иначе передаёт запрос следующему.
    /// </summary>
    ValidationResult Validate(PointOfInterest poi);
}