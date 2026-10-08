namespace InteractiveMap.Console.Validation;

/// <summary>
/// Результат валидации: успех либо сообщение об ошибке.
/// </summary>
public sealed class ValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private ValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static ValidationResult Ok() => new(true, null);

    public static ValidationResult Fail(string message) => new(false, message);

    public override string ToString() =>
        IsValid ? "OK" : $"FAIL: {ErrorMessage}";
}