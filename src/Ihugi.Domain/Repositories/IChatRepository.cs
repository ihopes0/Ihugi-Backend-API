using Ihugi.Domain.Entities.Chats;

namespace Ihugi.Domain.Repositories;

/// <summary>
/// Репозиторий сущности Chat
/// </summary>
public interface IChatRepository : IRepository<Chat>
{
    Task<Chat?> GetByIdWithMessagesAsync(Guid id, CancellationToken cancellationToken);
}