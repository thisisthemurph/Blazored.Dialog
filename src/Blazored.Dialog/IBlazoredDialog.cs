namespace Blazored.Dialog;

public interface IBlazoredDialog
{
    /// <summary>
    /// Sets the ID of the HTMLDialogElement
    /// </summary>
    void SetId(string htmlDialogId);

    /// <summary>
    /// Sets the return value of the HTMLDialogElement
    /// </summary>
    /// <param name="returnValue">The value to be set</param>
    void SetReturnValue(string returnValue);

    /// <summary>
    /// Retrieves the returnValue associated with the HTMLDialogElement
    /// </summary>
    /// <returns></returns>
    Task<string> GetReturnValue();
    
    /// <summary>
    /// Displays the dialog modelessly, i.e. still allowing interaction with content outside of the dialog.
    /// </summary>
    Task Show();
    
    /// <summary>
    /// Displays the dialog as a modal, over the top of any other dialogs that might be present.
    /// It displays into the top layer, along with a ::backdrop pseudo-element.
    /// Interaction outside the dialog is blocked and the content outside it is rendered inert.
    /// </summary>
    Task ShowModal();
    
    /// <summary>
    /// Closes the HTMLDialogElement
    /// </summary>
    Task Close();

    /// <summary>
    /// Closes the HTMLDialogElement, setting the returnValue of the HTMLDialogElement
    /// </summary>
    /// <param name="returnValue">The value to be set</param>
    Task Close(string returnValue);

    /// <summary>
    /// Determines if the HTMLDialogElement is currently open or not
    /// </summary>
    /// <returns>True if the dialog is open otherwise False</returns>
    Task<bool> IsOpen();
}
