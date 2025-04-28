using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Repositories;

namespace Ihugi.Infrastructure.Repositories;

// TODO: XML docs
// TODO: Подумать поменять ли с дженерик репозитория на конкретные
internal class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    public ChatRepository(AppDbContext context) : base(context)
    {
    }
}