using Microsoft.JSInterop;

namespace Blazored.Dialog;

public class BlazoredDialog : IBlazoredDialog, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private string? DialogId { get; set; }
    
    public BlazoredDialog(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Blazored.Dialog/blazoredDialog.js").AsTask());
    }

    public void SetId(string htmlDialogId)
    {
        DialogId = htmlDialogId;
    }

    public async Task Show()
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.show", DialogId);
    }

    public async Task ShowModal()
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.showModal", DialogId);
    }

    public async Task Close()
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.close", DialogId);
    }
    
    public async Task Close(string returnValue)
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.closeWithReturnValue", DialogId, returnValue);
    }

    public async Task<bool> IsOpen()
    {
        if (await DialogIdIsNull()) return false;
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("blazoredDialog.isOpen", DialogId);
    }

    public async void SetReturnValue(string returnValue)
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.setReturnValue", DialogId, returnValue);
    }

    public async Task<string> GetReturnValue()
    {
        if (await DialogIdIsNull()) return string.Empty;
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("blazoredDialog.getReturnValue", DialogId);
    }

    private async Task<bool> DialogIdIsNull()
    {
        if (DialogId is null)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync(
                "console.error", 
                "Cannot interact with HTMLDialogElement. Ensure the id of the HTML element has been set using the SetId method.");
        }

        return DialogId is null;
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