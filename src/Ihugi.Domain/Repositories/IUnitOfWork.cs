namespace Ihugi.Domain.Repositories;

// TODO: XML docs
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}