namespace Blazored.Dialog;

public interface IBlazoredDialogService
{
    /// <summary>
    /// Creates a new instance of a BlazoredDialog with the DialogId associated with the given htmlDialogId.
    /// </summary>
    /// <param name="htmlDialogId">The id to be associated with the HTMLDialogElement.</param>
    /// <returns>A new instance of DialogElement.</returns>
    BlazoredDialog NewDialog(string htmlDialogId);
}
