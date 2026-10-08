using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// ConcreteHandler: проверяет рейтинг (если указан).
/// </summary>
public sealed class RatingValidator : AbstractPoiValidator
{
    public override ValidationResult Validate(PointOfInterest poi)
    {
        if (poi.Rating.HasValue && (poi.Rating.Value < 0 || poi.Rating.Value > 5))
        {
            return ValidationResult.Fail($"Рейтинг {poi.Rating.Value} вне диапазона [0; 5].");
        }

        return PassToNext(poi);
    }
}