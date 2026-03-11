using Contract.Services;
using Microsoft.Extensions.DependencyInjection;
using Plume.Application.Services;

namespace Plume.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        return services;
    }
}
