using Ihugi.Domain.Entities;

namespace Ihugi.Domain.Repositories;

/// <summary>
/// Репозиторий сущности User
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Проверяет уникальность электронной почты в БД
    /// </summary>
    /// <param name="email">Электронная почта для проверки уникальности</param>
    /// <param name="cancellationToken">Токен остановки асинхронной операции</param>
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пользователя по email
    /// </summary>
    /// <param name="email">Электронная почта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}