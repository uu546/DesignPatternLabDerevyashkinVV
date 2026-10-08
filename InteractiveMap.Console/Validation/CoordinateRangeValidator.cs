
using InteractiveMap.Console.Models;

namespace InteractiveMap.Console.Validation;

/// <summary>
/// ConcreteHandler: проверяет, что координаты в допустимых диапазонах.
/// </summary>
public sealed class CoordinateRangeValidator : AbstractPoiValidator
{
    public override ValidationResult Validate(PointOfInterest poi)
    {
        double lat = poi.Coordinate.Latitude;
        double lon = poi.Coordinate.Longitude;

        if (lat < -90 || lat > 90)
        {
            return ValidationResult.Fail($"Широта {lat} вне диапазона [-90; 90].");
        }

        if (lon < -180 || lon > 180)
        {
            return ValidationResult.Fail($"Долгота {lon} вне диапазона [-180; 180].");
        }

        return PassToNext(poi);
    }
}