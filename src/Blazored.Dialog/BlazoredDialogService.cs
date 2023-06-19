using System.Reflection;
using Microsoft.JSInterop;

namespace Blazored.Dialog;

public class BlazoredDialogService : IBlazoredDialogService, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly string _assemblyName;
    
    public BlazoredDialogService(IJSRuntime jsRuntime, string assemblyName)
    {
        _assemblyName = assemblyName ?? throw new ArgumentNullException(assemblyName);
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Blazored.Dialog/blazoredDialog.js").AsTask());
    }

    public BlazoredDialog NewDialog(string htmlDialogId)
    {
        return new BlazoredDialog(htmlDialogId, _moduleTask, _assemblyName);
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}