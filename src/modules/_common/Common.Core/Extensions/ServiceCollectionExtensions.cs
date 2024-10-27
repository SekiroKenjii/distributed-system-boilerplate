using Common.Core.Abstractions.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Core.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Binds configuration sections to the specified configuration types.
    /// </summary>
    /// <typeparam name="TConfig">The interface type of the configuration.</typeparam>
    /// <typeparam name="TConfigImplement">The implementation type of the configuration.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection" /> to add configurations to.</param>
    /// <param name="configuration">The <see cref="IConfiguration" /> to use for loading configurations.</param>
    public static void BindConfigurations<TConfig, TConfigImplement>(this IServiceCollection services,
                                                                     IConfiguration configuration)
        where TConfig : class, IOptionRoot
        where TConfigImplement : class, TConfig
    {
        services.Configure<TConfigImplement>(configuration.GetSection(typeof(TConfigImplement).Name));
        services.AddSingleton<TConfig>(
            serviceProvider => serviceProvider.GetRequiredService<IOptions<TConfigImplement>>().Value
        );
    }
}