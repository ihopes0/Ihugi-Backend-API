namespace Ihugi.Common.ErrorWork;

/// <summary>
/// Generic Result type
/// </summary>
/// <typeparam name="TValue">Resulted operation response value type</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;
    
    /// <summary>
    /// Initializes a new instance of Result
    /// </summary>
    /// <param name="value">Returned value</param>
    /// <param name="isSuccess">Shows status of the operation</param>
    /// <param name="error">Error</param>
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value => IsSuccess
        ? _value
        : default;

    /// <summary>
    /// Create a new Result type object instance
    /// </summary>
    /// <param name="value">Возвращаемое значение</param>
    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}