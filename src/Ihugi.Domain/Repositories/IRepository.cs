using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Repositories;

/// <summary>
/// Рерозиторий сущности
/// </summary>
/// <typeparam name="TEntity">Корень агрегата</typeparam>
public interface IRepository<TEntity>
    where TEntity : AggregateRoot
{
    /// <summary>
    /// Получить запись по уникальному ID
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все записи
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить запись
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="entity">Удаляемая сущность</param>
    void Delete(TEntity entity);
    
    /// <summary>
    /// Обновить запись
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    void Update(TEntity entity);
}