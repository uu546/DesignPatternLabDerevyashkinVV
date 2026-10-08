using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// Базовый обработчик с готовой логикой передачи по цепочке.
/// </summary>
public abstract class AbstractPoiValidator : IPoiValidator
{
    protected IPoiValidator? _next;

    public IPoiValidator SetNext(IPoiValidator next)
    {
        _next = next;
        return next;
    }

    public abstract ValidationResult Validate(PointOfInterest poi);

    /// <summary>
    /// Передаёт запрос следующему обработчику.
    /// Если следующего нет — считаем проверку успешной
    /// (цепочка не нашла ошибок).
    /// </summary>
    protected ValidationResult PassToNext(PointOfInterest poi)
    {
        if (_next is null)
        {
            return ValidationResult.Ok();
        }

        return _next.Validate(poi);
    }
}