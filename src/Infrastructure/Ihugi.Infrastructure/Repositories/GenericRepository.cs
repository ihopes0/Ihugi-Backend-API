using Ihugi.Domain.Primitives;
using Ihugi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ihugi.Infrastructure.Repositories;

/// <summary>
/// Обобщенный репозиторий сущности
/// </summary>
/// <typeparam name="TEntity">Корень агрегата</typeparam>
internal abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
{
    protected readonly DbSet<TEntity> DbSet;

    protected GenericRepository(AppDbContext context)
    {
        DbSet = context.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}