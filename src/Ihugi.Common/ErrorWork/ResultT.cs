namespace Ihugi.Common.ErrorWork;

/// <summary>
/// Обобщенный результат
/// </summary>
/// <typeparam name="TValue">Тип возвращаемого значения</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;
    
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="value">Возвращаемое значение</param>
    /// <param name="isSuccess">Успешность выполненной операции</param>
    /// <param name="error">Ошибка</param>
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Невозможно получить доступ к значению неудачного результата.");

    /// <summary>
    /// Присвоение конструктору обобщенного результата статичный метод обычного результата
    /// </summary>
    /// <param name="value">Возвращаемое значение</param>
    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}