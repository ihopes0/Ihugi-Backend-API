using System.Collections.Concurrent;
using System.Text.Json;
using Ihugi.Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace Ihugi.Infrastructure.Caching;

/// <inheritdoc/>
public class CacheService : ICacheService
{
    private readonly ConcurrentBag<string> _cacheKeys = [];

    private readonly IDistributedCache _cache;

    /// <summary>
    /// .ctor
    /// </summary>
    public CacheService(IDistributedCache distributedCache)
    {
        _cache = distributedCache;
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await _cache.GetStringAsync(key, cancellationToken);

        return cachedValue is null ? null : JsonSerializer.Deserialize<T>(cachedValue);
    }

    /// <inheritdoc/>
    public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        where T : class
    {
        T? cachedValue = await GetAsync<T>(key, cancellationToken);

        if (cachedValue is not null)
        {
            return cachedValue;
        }

        cachedValue = await factory();

        await SetAsync(key, cachedValue, cancellationToken);

        return cachedValue;
    }

    /// <inheritdoc/>
    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        var valueToCache = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(key, valueToCache, cancellationToken);

        _cacheKeys.Add(key);
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(key, cancellationToken);

        _cacheKeys.TryTake(out var _);
    }

    /// <inheritdoc/>
    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = _cacheKeys
            .Where(k => k.StartsWith(prefixKey))
            .Select(k => RemoveAsync(k, cancellationToken));

        await Task.WhenAll(tasks);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetByPrefixAsync<T>(string prefixKey,
        CancellationToken cancellationToken = default)
        where T : class
    {
        var cachedValues = new ConcurrentBag<T>();

        var tasks = _cacheKeys
            .Where(k => k.StartsWith(prefixKey))
            .Select(async k =>
            {
                var value = await GetAsync<T>(k, cancellationToken);
                if (value != null)
                {
                    cachedValues.Add(value);
                }
            });

        await Task.WhenAll(tasks);

        return cachedValues;
    }
}