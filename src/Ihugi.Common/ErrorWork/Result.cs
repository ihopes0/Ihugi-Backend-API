namespace Ihugi.Common.ErrorWork;

/// <summary>
/// Результат работы UseCase (комманды или запроса) 
/// </summary>
public class Result
{
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="isSuccess">Успешность операции</param>
    /// <param name="error">Объект ошибки</param>
    /// <exception cref="InvalidOperationException">Непредвиденное поведение</exception>
    protected internal Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();
            case false when error == Error.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }
    
    /// <summary>
    /// Успешность операции
    /// </summary>
    public bool IsSuccess { get; }
    
    /// <summary>
    /// Провал операции
    /// </summary>
    public bool IsFailure => !IsSuccess;
    
    /// <summary>
    /// Ошибка, возникшая при выполнении операции
    /// </summary>
    public Error Error { get; }
    
    /// <summary>
    /// Возвращает успешный результат
    /// </summary>
    public static Result Success() => new(true, Error.None);
    
    /// <summary>
    /// Возвращает обобщенный успешный результат
    /// </summary>
    /// <param name="value">Возвращенное значение</param>
    /// <typeparam name="TValue">Тип возвращаемого значения</typeparam>
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    /// <summary>
    /// Возвращает результат с неудачей
    /// </summary>
    /// <param name="error">Ошибка, которая возникла во время выполнения опреации</param>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Возвращает обобщенный результат с неудачей
    /// </summary>
    /// <param name="error">Ошибка, которая возникла во время выполнения опреации</param>
    /// <typeparam name="TValue">Тип возвращаемого значения</typeparam>
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    /// <summary>
    /// Создать типизированный экземпляр результата
    /// </summary>
    /// <param name="value">Возвращаемое значение</param>
    /// <typeparam name="TValue">Тип возвращаемого значения</typeparam>
    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}