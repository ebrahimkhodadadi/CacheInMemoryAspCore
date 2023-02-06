
using CacheService.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
namespace CacheService;

public static class ServiceExtensions
{
    public static void AddCacheService(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));
    }
}
