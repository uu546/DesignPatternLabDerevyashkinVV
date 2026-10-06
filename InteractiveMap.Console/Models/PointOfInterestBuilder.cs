namespace InteractiveMap.Console.Models;

public class PointOfInterestBuilder
{
    private string? _name;
    private Coordinate? _coordinate;
    private string? _description;
    private string? _category;
    private double? _rating;
    private string? _openingHours;

    public PointOfInterestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PointOfInterestBuilder WithCoordinate(Coordinate coordinate)
    {
        _coordinate = coordinate;
        return this;
    }

    public PointOfInterestBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public PointOfInterestBuilder WithCategory(string category)
    {
        _category = category;
        return this;
    }

    public PointOfInterestBuilder WithRating(double rating)
    {
        _rating = rating;
        return this;
    }

    public PointOfInterestBuilder WithOpeningHours(string openingHours)
    {
        _openingHours = openingHours;
        return this;
    }

    public PointOfInterest Build()
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            throw new InvalidOperationException("Не задано имя точки интереса (WithName).");
        }

        if (_coordinate is null)
        {
            throw new InvalidOperationException("Не задана координата точки интереса (WithCoordinate).");
        }

        if (_rating is < 0 or > 5)
        {
            throw new InvalidOperationException("Рейтинг должен быть в диапазоне [0; 5].");
        }

        return new PointOfInterest
        {
            Name = _name.Trim(),
            Coordinate = _coordinate,
            Description = _description?.Trim(),
            Category = _category,
            Rating = _rating,
            OpeningHours = _openingHours?.Trim()
        };
    }
}
