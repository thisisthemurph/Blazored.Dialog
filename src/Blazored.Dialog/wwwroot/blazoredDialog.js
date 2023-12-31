/** 
 * @param {string} dialogId the ID associated with the HTMLDialogElement
 * @return {HTMLDialogElement}
 */
const tryGetHtmlDialogElementOrThrow = (dialogId) => {
    /** @type HTMLDialogElement */
    const dialog = document.getElementById(dialogId);
    if (!dialog) {
        throw new Error(`BlazoredDialog Exception: HTMLDialogElement with id="${dialogId}" does not exist.`)
    }
    
    return dialog;
}
/**
 * @name OnCloseCallbackFn
 * @function
 * @returns {Promise<void>}
 */
/** @type {OnCloseCallbackFn[]} */
const eventListenerMethodList = [];

/**
 * @type {{
 *  show: function(string): (void), 
 *  showModal: function(string): (void), 
 *  close: function(string): (void),
 *  closeWithReturnValue: function(string, string): (void),
 *  isOpen: function(string): (boolean), 
 *  setReturnValue: function(string, string): (void), 
 *  getReturnValue: function(string): (string),
 *  addCallback: function(string, string, string): (void),
 *  cleanupOnCloseEventListeners: function(string): (void),
 *  }} 
 */
export const blazoredDialog = {
    show: (dialogId) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        dialog.show();
    },
    showModal: (dialogId) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        dialog.showModal();
    },
    close: (dialogId) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        dialog.close();
    },
    closeWithReturnValue: (dialogId, returnValue) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        dialog.close(returnValue);
    },
    isOpen: (dialogId) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        return dialog.open;
    },
    setReturnValue: (dialogId, returnValue) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        dialog.returnValue = returnValue;
    },
    getReturnValue: (dialogId) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        return dialog.returnValue;
    },
    addCallback: (dialogId, assemblyName, callbackMethodName) => {
        const callbackFn = async () => {
            await DotNet.invokeMethodAsync(assemblyName, callbackMethodName);
        }
        
        eventListenerMethodList.push(callbackFn);
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        dialog.addEventListener("close", callbackFn);
    },
    cleanupOnCloseEventListeners: (dialogId) => {
        const dialog = tryGetHtmlDialogElementOrThrow(dialogId);
        eventListenerMethodList.forEach((callbackFn) => {
            dialog.removeEventListener("close", callbackFn);
        })
    }
}
