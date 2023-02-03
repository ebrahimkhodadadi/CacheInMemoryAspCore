
using CacheService.Utilities;
using System.Runtime.Caching;

namespace CacheService.Repository;

public class CacheService<T> : ICacheService<T> where T : CacheBaseEntity
{
    ObjectCache _memoryCache = MemoryCache.Default;
    private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _MemoryCache;

    private readonly ILogger<CacheService<T>> _Logger;
    public CacheService(ILogger<CacheService<T>> logger, Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache)
    {
        _Logger = logger;
        _MemoryCache = memoryCache;
    }

    public List<KeyValuePair<string, object>> FilteryBy(Expression<Func<KeyValuePair<string, object>, bool>> predicate)
    {
        var exp = predicate.Compile();

        return _memoryCache.AsQueryable().Where(x => x.Key.Contains(typeof(T).Name) && exp(x)).ToList();
    }

    public T Add(T value, DateTimeOffset expirationTime)
    {
        try
        {
            Assert.NotNull(value, nameof(value));

            value.Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
            value.Created = DateTime.Now;

            _memoryCache.Set($"{typeof(T).Name}:{value.Id}", value, expirationTime);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, $"Error while adding {value}");
            throw;
        }
        return value;
    }

    public T Update(string Id, T value, DateTimeOffset expirationTime)
    {
        try
        {
            Assert.NotNull(value, nameof(value));
            if (!_MemoryCache.TryGetValue($"{typeof(T).Name}:{Id}", out object values))
                Assert.NotNull(Id, nameof(Id));

            value.Modified = DateTime.Now;

            _memoryCache.Set($"{typeof(T).Name}:{Id}", value, expirationTime);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, $"Error while Updating {value}");
            throw;
        }
        return value;
    }

    public bool Delete(string Id)
    {
        try
        {
            Assert.NotNull(Id, nameof(Id));
            if (!_MemoryCache.TryGetValue($"{typeof(T).Name}:{Id}", out object values))
                Assert.NotNull(Id, nameof(Id));

            _memoryCache.Remove($"{typeof(T).Name}:{Id}");

            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, $"Error while Deleting {Id}");
            throw;
        }

        return false;
    }

    public List<T> GetAll()
    {
        return _memoryCache.AsQueryable().Where(x => x.Key.Contains(typeof(T).Name)).Select(x => (T)x.Value).ToList();
    }

    public T GetById(string Id)
    {
        try
        {
            Assert.NotNull(Id, nameof(Id));

            return (T)_memoryCache.Get($"{typeof(T).Name}:{Id}");

        }
        catch (Exception e)
        {
            _Logger.LogError(e, $"Error while Getting {Id}");
            throw;
        }
    }
}
