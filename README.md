# BlazoredDialog

BlazoredDialog is a utility for interfacing with HTML `<dialog>` elements on Blazor pages or components.

Generally, working with a `<dialog>` element would mean injecting `IJSRuntime` and interacting with JS via JSInterop; BlazoredDialog abstracts this and provides handy C# methods that do the work for you.

## What is a HTML dialog element?

The `<dialog>` HTML element represents a dialog box, modal, or other interactive component such as a dismissible alert, inspector, or subwindow.

[Read more at the MDN Web Docs](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/dialog)

# Configuration

Simply use the `builder.Services.AddBlazoredDialog();` method to enable the use of the `IBlazoredDialogService` via dependency injection.

```csharp
using Blazored.Dialog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazoredDialog();

await builder.Build().RunAsync();
```

Wrap your application, or any specific area to be targeted, with the `<CascadingBlazoredDialog>` in your `App.razor` component:

```razor
@using Blazored.Dialog

<CascadingBlazoredDialog>
    <Router AppAssembly="@typeof(App).Assembly">
        <Found Context="routeData">
            <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)"/>
            <FocusOnNavigate RouteData="@routeData" Selector="h1"/>
        </Found>
        <NotFound>
            <PageTitle>Not found</PageTitle>
            <LayoutView Layout="@typeof(MainLayout)">
                <p role="alert">Sorry, there's nothing at this address.</p>
            </LayoutView>
        </NotFound>
    </Router>
</CascadingBlazoredDialog>
```

# Usage

## Setup

At the top of the page or component, implement the `using` directive:

```razor
@page "/dialog"
@using Blazored.Dialog
```

## Register a HTML dialog element

To use BlazoredDialog within a page or component:
- Retrieve the `IBlazoredDialogService` from the cascaded value
- Create an initial empty `BlazoredDialog`
- Initialize the dialog within a lifecycle method using the `NewDialog` method, passing the unique id you would like to use

Note that in the below example we have passed `@_dialog.DialogId` to the `HTMLDialogElement`. We could jsut as easily have passed the string `myDialog`.

```razor
<dialog id="@_dialog.DialogId">
    <h2>Hey, I'm an HTML dialog element!</h2>
    <p>This is some interesting information I have here!</p>
</dialog>

@code
{
    [CascadingParameter] public IBlazoredDialogService DialogService { get; set; } = default!;
    private BlazoredDialog _dialog = default!;

    protected override void OnInitialized()
    {
        _dialog = DialogService.NewDialog("myDialog");
    }
}
```

## Opening a dialog element

To display the dialog modelessly, i.e. still allowing interaction with content outside of the dialog:

```razor
<button @onclick="@(() => _dialog.Show())">
    Open Dialog Element
</button>
```

To display the dialog as a modal, over the top of any other dialogs that might be present.

```razor
<button @onclick="@(() => Dialog.ShowModal())">
    Open Dialog Element
</button>
```

Note that when using this method everything outside the dialog is inert with interactions outside the dialog being blocked.

## Closing a dialog element

There are many ways to close a `<dialog>` modal and the method chosen will depend on the use case. It is recommended that the dialog documentation is read to understand which is best suited.

A dialog can be programmatically closed by using the `.Close()` method.

```razor
<dialog id="myDialog">
    <h2>Whoa buddy!</h2>
    <p>Are you sure you meant to click that button?</p>
    <button @onclick="@(() => _dialog.Close())">
        Close
    </button>
</dialog>
```

# Examples

Examples have been provided within the examples directory of the project.

# Methods

# IBlazoredDialogService

| Method | Return Type | Description |
|---|---|---|
| NewDialog(string htmlDialogId) | `BlazoredDialog` | Creates a new instance of a `BlazoredDialog` with the DialogId associated with the given `htmlDialogId` |

# BlazoredDialog

| Method | Return Type | Description |
|---|---|---|
| Show() | `Task` | Displays the dialog modelessly, i.e. still allowing interaction with content outside of the dialog. |
| ShowModal() | `Task` | Displays the dialog as a modal, over the top of any other dialogs that might be present. It displays in the top layer, along with a `::backdrop` pseudo-element. Interaction outside the dialog is blocked and the content outside it is rendered inert. |
| Close() | `Task` | Closes the `HTMLDialogElement`. |
| Close(string returnValue) | `Task` | Closes the `HTMLDialogElement` and updates the `returnValue` of the dialog. |
| IsOpen() | `Task<bool>` | Determines if the `<dialog>` is open or not. |
| SetReturnValue(string returnValue) | `void` | Sets the return value for the `<dialog>`, usually to indicate which button the user pressed to close it. |
| GetReturnValue() | `Task<string>` | Gets the return value for the `<dialog>`, usually to indicate which button the user pressed to close it. |













