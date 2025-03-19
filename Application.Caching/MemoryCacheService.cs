using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Company_Expense_Tracker.Application.Caching;

public static class MemoryCacheService
{
    private static readonly DistributedCacheEntryOptions Default = new ()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };
    
    private static readonly SemaphoreSlim SemaphoreSlim = new (1, 1);

    public static async Task<T?> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<CancellationToken, Task<T>> factory,
        DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await cache.GetStringAsync(key, cancellationToken);

        T? value;
        if (!string.IsNullOrWhiteSpace(cachedValue))
        {
            value = JsonSerializer.Deserialize<T>(cachedValue);
            
            if (value is not null)
            {
                return value;
            }
        }

        var hasLock = await SemaphoreSlim.WaitAsync(500);

        if (!hasLock)
        { 
            return default;
        }

        try
        {
            cachedValue = await cache.GetStringAsync(key, cancellationToken);
            if (!string.IsNullOrWhiteSpace(cachedValue))
            {
                value = JsonSerializer.Deserialize<T>(cachedValue);
            
                if (value is not null)
                {
                    return value;
                }
            }
            
            value = await factory(cancellationToken);

            if (value is null)
            {
                return default;
            }

            await cache.SetStringAsync(key, JsonSerializer.Serialize(value), options ?? Default , cancellationToken);
        }
        
        finally
        {
            SemaphoreSlim.Release();
        }
        
        return value;
    }
}