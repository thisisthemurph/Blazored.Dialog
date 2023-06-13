namespace Blazored.Dialog;

public interface IBlazoredDialog
{
    /// <summary>
    /// Creates a new instance of BlazoredDialog
    /// </summary>
    /// <param name="htmlDialogId">The id of the HTMLDialogElement to be affected</param>
    /// <returns>BlazoredDialog instance</returns>
    BlazoredDialog CreateNewDialog(string htmlDialogId);
}
