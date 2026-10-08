using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// Client в паттерне Цепочка обязанностей:
/// собирает цепочку валидаторов в нужном порядке
/// и запускает валидацию с головы цепочки.
/// </summary>
public sealed class PoiValidationPipeline
{
    private readonly IPoiValidator _head;

    public PoiValidationPipeline()
    {
        NameValidator name = new();
        CoordinateRangeValidator coordinate = new();
        RatingValidator rating = new();
        CategoryValidator category = new();

        // Собираем цепочку: name -> coordinate -> rating -> category.
        name.SetNext(coordinate)
            .SetNext(rating)
            .SetNext(category);

        _head = name;
    }

    public ValidationResult Validate(PointOfInterest poi)
    {
        return _head.Validate(poi);
    }
}