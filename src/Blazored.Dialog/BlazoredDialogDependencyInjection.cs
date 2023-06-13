using Microsoft.Extensions.DependencyInjection;

namespace Blazored.Dialog;

public static class BlazoredDialogDependencyInjection
{
    public static IServiceCollection AddBlazoredDialog(this IServiceCollection services)
    {
        services.AddScoped<IBlazoredDialog, BlazoredDialogService>();
        return services;
    }
}