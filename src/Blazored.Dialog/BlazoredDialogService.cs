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

    /// <summary>
    /// Creates a new instance of a BlazoredDialog with the DialogId associated with the given htmlDialogId.
    /// </summary>
    /// <param name="htmlDialogId">The id to be associated with the HTMLDialogElement.</param>
    /// <returns>A new instance of DialogElement.</returns>
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