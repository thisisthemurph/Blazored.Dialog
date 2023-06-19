using Microsoft.JSInterop;

namespace Blazored.Dialog;

public class BlazoredDialog
{
    public string? DialogId { get; init; }
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    protected BlazoredDialog()
    {
        DialogId = default;
        _moduleTask = default!;
    }
    
    internal BlazoredDialog(string htmlDialogId, Lazy<Task<IJSObjectReference>> moduleTask)
    {
        DialogId = htmlDialogId;
        _moduleTask = moduleTask;
    }

    /// <summary>
    /// Displays the dialog modelessly, i.e. still allowing interaction with content outside of the dialog.
    /// </summary>
    public async Task Show()
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.show", DialogId);
    }

    /// <summary>
    /// Displays the dialog as a modal, over the top of any other dialogs that might be present. It displays in the
    /// top layer, along with a ::backdrop pseudo-element. Interaction outside the dialog is blocked and the content
    /// outside it is rendered inert.
    /// </summary>
    public async Task ShowModal()
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.showModal", DialogId);
    }

    /// <summary>
    /// Closes the dialog.
    /// </summary>
    public async Task Close()
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.close", DialogId);
    }
    
    /// <summary>
    /// Closes the HTMLDialogElement and updates the returnValue of the dialog.
    /// </summary>
    /// <param name="returnValue">Value to be stored in the dialog returnValue.</param>
    public async Task Close(string returnValue)
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.closeWithReturnValue", DialogId, returnValue);
    }

    /// <summary>
    /// Determines if the dialog is open or not.
    /// </summary>
    /// <returns>True if the dialog is open, otherwise False</returns>
    public async Task<bool> IsOpen()
    {
        if (await DialogIdIsNull()) return false;
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("blazoredDialog.isOpen", DialogId);
    }

    /// <summary>
    /// Sets the return value for the dialog, usually to indicate which button the user pressed to close it.
    /// </summary>
    /// <param name="returnValue">Value to be stored in the dialog returnValue.</param>
    public async void SetReturnValue(string returnValue)
    {
        if (await DialogIdIsNull()) return;
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("blazoredDialog.setReturnValue", DialogId, returnValue);
    }

    /// <summary>
    /// Gets the return value for the dialog, usually to indicate which button the user pressed to close it.
    /// </summary>
    /// <returns>The returnValue associated with the dialog.</returns>
    public async Task<string> GetReturnValue()
    {
        if (await DialogIdIsNull()) return string.Empty;
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("blazoredDialog.getReturnValue", DialogId);
    }

    public async Task OnClose(string assemblyName, string callbackMethodName)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(
            "blazoredDialog.addCallback", 
            DialogId, 
            assemblyName, 
            callbackMethodName);
    }

    /// <summary>
    /// Determines if the DialogId is null (has not been set) and logs an error to the web console if not.
    /// </summary>
    /// <returns>True if DialogId has been set, otherwise False.</returns>
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
}