using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// ConcreteHandler: проверяет категорию (если указана).
/// </summary>
public sealed class CategoryValidator : AbstractPoiValidator
{
    public override ValidationResult Validate(PointOfInterest poi)
    {
        if (poi.Category is not null && string.IsNullOrWhiteSpace(poi.Category))
        {
            return ValidationResult.Fail("Категория указана, но значение пустое ' '.");
        }

        return PassToNext(poi);
    }
}