namespace Ihugi.Common.ErrorWork;

/// <summary>
/// Ошибка. Ипользуется для отображения ошибки в классе Result
/// </summary>
public class Error : IEquatable<Error>
{
    /// <summary>
    /// Отображает отсутствие ошибки
    /// </summary>
    public static readonly Error None = new(String.Empty, String.Empty);
    
    /// <summary>
    /// Ошибка для null значения
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "The specified value is null.");

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="code">Код ошибки</param>
    /// <param name="message">Сообщение ошибки</param>
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Код ошибки
    /// </summary>
    public string Code { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? first, Error? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Error? first, Error? second)
    {
        return !(first == second);
    }

    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj)
    {
        return obj is Error error && Equals(error);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString()
    {
        return Code;
    }
}