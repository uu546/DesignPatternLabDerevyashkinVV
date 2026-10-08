
using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// ConcreteHandler: проверяет, что имя не пустое.
/// </summary>
public sealed class NameValidator : AbstractPoiValidator
{
    public override ValidationResult Validate(PointOfInterest poi)
    {
        if (string.IsNullOrWhiteSpace(poi.Name))
        {
            return ValidationResult.Fail("Имя точки не может быть пустым.");
        }

        return PassToNext(poi);
    }
}