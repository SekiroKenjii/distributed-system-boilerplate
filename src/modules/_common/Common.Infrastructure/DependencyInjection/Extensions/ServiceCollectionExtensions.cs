using Common.Core.Abstractions.Cache;
using Common.Core.Abstractions.Email;
using Common.Core.Abstractions.Serializer;
using Common.Core.Abstractions.System;
using Common.Core.Extensions;
using Common.Infrastructure.DependencyInjection.Options;
using Common.Infrastructure.Services.Cache;
using Common.Infrastructure.Services.Email;
using Common.Infrastructure.Services.Serializer;
using Common.Infrastructure.Services.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.DependencyInjection.Extensions;

/// <summary>
///     Extension methods for adding common infrastructure services to the <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds common infrastructure services to the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration" /> to use for configuring services.</param>
    public static void AddCommonInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadCommonInfrastructureConfigs(configuration);
        services.AddDistributedCacheServices(configuration);
        services.AddCommonServices();
        services.AddCommonCronJobs();
    }

    /// <summary>
    ///     Loads common infrastructure configurations from the <see cref="IConfiguration" /> and binds them to the
    ///     <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add configurations to.</param>
    /// <param name="configuration">The <see cref="IConfiguration" /> to use for loading configurations.</param>
    private static void LoadCommonInfrastructureConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.BindConfigurations<IEmailConfiguration, EmailConfiguration>(configuration);
    }

    /// <summary>
    ///     Adds distributed cache services to the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add cache services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration" /> to use for configuring cache services.</param>
    private static void AddDistributedCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options => {
            var connectionString = configuration.GetConnectionString("RedisConnectionString");
            options.Configuration = connectionString;
        });

        services
           .AddSingleton<ICacheService, CacheService>()
           .AddSingleton<ICacheInvalidationService, CacheInvalidationService>();
    }

    /// <summary>
    ///     Adds common services to the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    private static void AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddTransient<ISerializerService, NewtonSoftService>();
        services.AddScoped<ISmtpClientService, SmtpClientService>();
        services.AddScoped<IEmailService, EmailService>();
    }

    /// <summary>
    ///     Adds common cron jobs to the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add cron jobs to.</param>
    private static void AddCommonCronJobs(this IServiceCollection services)
    {
        // register cron jobs here
    }
}