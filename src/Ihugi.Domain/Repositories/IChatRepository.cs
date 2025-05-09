using Ihugi.Domain.Entities.Chats;

namespace Ihugi.Domain.Repositories;

/// <summary>
/// Репозиторий сущности Chat
/// </summary>
public interface IChatRepository : IRepository<Chat>
{
    /// <summary>
    /// Получить чат с его сообщениями
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Chat?> GetByIdWithMessagesAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить чат с его сообщениями и списком участников
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Chat?> GetByIdWithMembersAsync(Guid id, CancellationToken cancellationToken);
}