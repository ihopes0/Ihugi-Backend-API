namespace Ihugi.Domain.Primitives;

/// <summary>
/// Доменная сущность
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    protected Entity()
    {}
    
    /// <summary>
    /// Конструктор с инициализацией Id cущности
    /// </summary>
    /// <param name="id">Id сущности</param>
    protected Entity(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; private init; }

    /// <summary>
    /// Оператор сравнения равенства
    /// </summary>
    /// <param name="first">Левая сущность</param>
    /// <param name="second">Правая сущность</param>
    /// <returns>True, если сущности идентичны; False, если сущнсости не идентичны</returns>
    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    /// <summary>
    /// Оператор сравнения неравенства
    /// </summary>
    /// <param name="first">Левая сущность</param>
    /// <param name="second">Правая сущность</param>
    /// <returns>True, если сущности не идентичны; False, если сущнсости идентичны</returns>
    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }
    
    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}