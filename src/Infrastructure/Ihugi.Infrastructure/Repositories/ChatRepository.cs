using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Repositories;

namespace Ihugi.Infrastructure.Repositories;

/// <inheritdoc cref="IChatRepository"/>
internal class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="context">Контекст БД</param>
    public ChatRepository(AppDbContext context) : base(context)
    {
    }
}