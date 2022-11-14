using Microsoft.AspNetCore.Mvc;
using Tutorials.Infrastructure.Persistence;
using Tutorials.WebApi.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy",
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:5173");
                              });
        });

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}
