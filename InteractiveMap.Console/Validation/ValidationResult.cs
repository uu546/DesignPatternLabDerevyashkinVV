namespace InteractiveMap.Console.Validation;

/// <summary>
/// Результат валидации: успех либо сообщение об ошибке.
/// </summary>
public sealed class ValidationResult
{
    private bool _isValid { get; }
    private string? _errorMessage { get; }

    private ValidationResult(bool isValid, string? errorMessage)
    {
        _isValid = isValid;
        _errorMessage = errorMessage;
    }

    public static ValidationResult Ok() => new(true, null);

    public static ValidationResult Fail(string message) => new(false, message);

    public override string ToString() =>
        _isValid ? "OK" : $"FAIL: {_errorMessage}";
}