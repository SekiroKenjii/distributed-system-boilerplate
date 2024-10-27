using Microsoft.AspNetCore.Builder;

namespace Common.Infrastructure.DependencyInjection.Extensions;

/// <summary>
///     Extension methods for configuring the common request pipeline in a <see cref="WebApplication" />.
/// </summary>
public static class WebAppExtensions
{
    /// <summary>
    ///     Configures the common request pipeline for the specified <see cref="WebApplication" />.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication" /> to configure.</param>
    public static void ConfigureCommonRequestPipeline(this WebApplication app)
    {
        // configure common request pipeline here
    }
}