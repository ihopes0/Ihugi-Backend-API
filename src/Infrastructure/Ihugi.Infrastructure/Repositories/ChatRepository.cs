using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Chat?> GetByIdWithMessagesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}