using Microsoft.JSInterop;

namespace Blazored.Dialog;

public class BlazoredDialogService : IBlazoredDialogService, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    
    public BlazoredDialogService(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Blazored.Dialog/blazoredDialog.js").AsTask());
    }

    public BlazoredDialog NewDialog(string htmlDialogId)
    {
        return new BlazoredDialog(htmlDialogId, _moduleTask);
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