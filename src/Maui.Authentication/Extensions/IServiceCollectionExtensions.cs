using Maui.Authentication.Services;

namespace Maui.Authentication.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMauiAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<MauiAuthenticationService>();

        return services;
    }
}
