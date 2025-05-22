using Ihugi.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Ihugi.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        return services;
    }
}