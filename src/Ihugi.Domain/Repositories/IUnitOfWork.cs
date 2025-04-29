namespace Ihugi.Domain.Repositories;

/// <summary>
/// Блок работы
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохранить изменения в БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены асинхронной операции</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}