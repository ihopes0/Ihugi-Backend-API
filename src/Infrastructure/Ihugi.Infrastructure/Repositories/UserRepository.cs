using Ihugi.Domain.Entities;
using Ihugi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ihugi.Infrastructure.Repositories;

// TODO: XML docs
// TODO: Подумать поменять ли с дженерик репозитория на конкретные
internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
    {
        var user = await DbSet.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);

        return user is null;
    }
}