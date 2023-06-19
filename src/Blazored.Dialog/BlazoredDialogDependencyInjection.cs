using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Blazored.Dialog;

public static class BlazoredDialogDependencyInjection
{
    public static IServiceCollection AddBlazoredDialog(this IServiceCollection services, Assembly assembly)
    {
        var assemblyName = assembly.GetName().Name;
        if (assemblyName is null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        services.AddSingleton<IBlazoredDialogService>(provider 
            => new BlazoredDialogService(provider.GetService<IJSRuntime>(), assemblyName));
        
        return services;
    }
}