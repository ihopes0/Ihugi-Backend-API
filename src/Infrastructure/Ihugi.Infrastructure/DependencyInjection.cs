using Ihugi.Application.Abstractions;
using Ihugi.Domain.Repositories;
using Ihugi.Infrastructure.Caching;
using Ihugi.Infrastructure.Repositories;

namespace Ihugi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        
        // Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Caching
        services.AddSingleton<ICacheService, CacheService>();
        
        return services;
    }
}