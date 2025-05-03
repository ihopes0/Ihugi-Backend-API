using Ihugi.Application.Abstractions;
using Ihugi.Domain.Abstractions;
using Ihugi.Domain.ValueObjects;

namespace Ihugi.Infrastructure.RealTime;

// TODO: XML docs
public sealed class ConnectionManager : IConnectionManager
{
    private readonly ICacheService _cacheService;

    public ConnectionManager(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    
    public async Task AddConnectionAsync(string connectionKey, UserConnection userConnection)
    {
        await _cacheService.SetAsync(connectionKey, userConnection);
    }

    public async Task RemoveConnectionAsync(string connectionKey)
    {
        await _cacheService.RemoveAsync(connectionKey);
    }

    public async Task<UserConnection?> GetConnectionAsync(string connectionKey)
    {
        return await _cacheService.GetAsync<UserConnection>(connectionKey);
    }

    public async Task<IEnumerable<UserConnection>> GetAllConnectionsAsync(string prefix)
    {
        return await _cacheService.GetByPrefixAsync<UserConnection>(prefix);
    }
}