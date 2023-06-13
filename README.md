# BlazoredDialog

BlazoredDialog is a utility for interfacing with HTML `<dialog>` elements on Blazor pages or components.

Generally, working with a `<dialog>` element would mean injecting `IJSRuntime` and interacting with JS via JSInterop; BlazoredDialog abstracts this and provides handy C# methods that do the work for you.

## What is a HTML dialog element?

The `<dialog>` HTML element represents a dialog box, modal, or other interactive component such as a dismissible alert, inspector, or subwindow.

[Read more at the MDN Web Docs](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/dialog)

# Configuration

Simply use the `builder.Services.AddBlazoredDialog();` method to enable the use of BlazoredDialog via dependency injection.

```csharp
using BlazoredDialog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazoredDialog();

await builder.Build().RunAsync();
```

# Usage

## Setup

At the top of the page, implement the `using` directive and inject the `IBlazoredDialog` interface, giving it a descriptive name as this is how we will reference the dialog later.

```razor
@page "/dialog"
@using BlazoredDialog
@inject IBlazoredDialog Dialog
```

## Register a HTML dialog element

You must register the id of the `<dialog>` element bu using the `SetId` method. The value provided to the method must match the `id` given to the HTMLDialogElement.

```razor
<dialog id="myDialog">
    <h2>Hey, I'm a dialog element!</h2>
    <p>This is some interesting information I have here!</p>
</dialog>

@code {
    protected override async Task OnInitializedAsync()
    {
        Dialog.SetId("myDialog");
    }
}
```

## Opening a dialog element

To display the dialog modelessly, i.e. still allowing interaction with content outside of the dialog.

```razor
<button @onclick="@(() => Dialog.Show())">
    Open Dialog Element
</button>
```

To display the dialog as a modal, over the top of any other dialogs that might be present. Everything outside the dialog are inert with interactions outside the dialog being blocked.

```razor
<button @onclick="@(() => Dialog.ShowModal())">
    Open Dialog Element
</button>
```

## Closing a dialog element

There are many ways to close a `<dialog>` modal and the method chosen will depend on the use case. It is recommended that the dialog documentation is read to understand which is best suited.

A dialog can be programmatically closed by using the `.Close()` method.

```razor
<dialog id="myDialog">
    <h2>Whoa buddy!</h2>
    <p>Are you sure you meant to click that button?</p>
    <button @onclick="@(() => Dialog.Close())">
        Close
    </button>
</dialog>
```

# Example

The following code example demonstrates using two `<dialog>` elements on a Blazor page; the first dialog is opened by a button on the page, whereas the second is opened after a period of 10 seconds, though only if the first is not already open.

```razor
@page "/dialog"
@using BlazoredDialog
@inject IBlazoredDialog AlertDialog
@inject IBlazoredDialog NotificationDialog

<dialog id="alertDialog">
    <h2>Whoa buddy!</h2>
    <p>Are you sure you meant to click that button?</p>
    <button @onclick="@(() => AlertDialog.Close())">
        Close
    </button>
</dialog>

<dialog id="notificationDialog">
    <h2>You've been here a while...</h2>
    <p>Fancy signing up for our paid membership?</p>
    <button @onclick="@(() => NotificationDialog.Close())">
        Absolutely not
    </button>
</dialog>

<button @onclick="@(() => AlertDialog.ShowModal())">
    Open Alert Dialog Element
</button>

@code {
    protected override async Task OnInitializedAsync()
    {
        AlertDialog.SetId("alertDialog");
        NotificationDialog.SetId("notificationDialog");
    }

    protected override async Task OnParametersSetAsync()
    {
        await Task.Delay(10 * 1000);
        if (!await AlertDialog.IsOpen())
        {
            await NotificationDialog.ShowModal();
        }
    }
}
```

















