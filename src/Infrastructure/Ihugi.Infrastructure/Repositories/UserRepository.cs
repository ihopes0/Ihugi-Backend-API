using Ihugi.Domain.Entities;
using Ihugi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ihugi.Infrastructure.Repositories;

/// <inheritdoc cref="IUserRepository"/>
internal class UserRepository : GenericRepository<User>, IUserRepository
{
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="context">Контекст БД</param>
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    /// <inheritdoc />
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
    {
        var user = await DbSet.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);

        return user is null;
    }
    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}