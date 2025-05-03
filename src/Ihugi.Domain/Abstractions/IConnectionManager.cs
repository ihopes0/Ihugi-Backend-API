using Ihugi.Domain.ValueObjects;

namespace Ihugi.Domain.Abstractions;

// TODO: XML docs
public interface IConnectionManager
{
    Task AddConnectionAsync(string connectionKey, UserConnection userConnection);

    Task RemoveConnectionAsync(string connectionKey);

    Task<UserConnection?> GetConnectionAsync(string connectionKey);

    Task<IEnumerable<UserConnection>> GetAllConnectionsAsync(string prefix);
}