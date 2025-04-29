using Ihugi.Domain.Repositories;

namespace Ihugi.Infrastructure;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}