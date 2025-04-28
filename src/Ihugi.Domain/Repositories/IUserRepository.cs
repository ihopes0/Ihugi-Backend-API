using Ihugi.Domain.Entities;

namespace Ihugi.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
}