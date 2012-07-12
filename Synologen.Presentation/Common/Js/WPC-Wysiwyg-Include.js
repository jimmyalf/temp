function initDialog() { //Can this method be removed?
    var selection = getAttribute("selection");
    $('input').first().val(selection);
}

if (window.attachEvent) {
    window.attachEvent("onload", initDialog);
}
else if (window.addEventListener) {
    window.addEventListener("load", initDialog);
}

function getAttribute(attr) {
    return tinyMCEPopup.getWindowArg(attr);
}

function GetInputValue() {
    var select = getAttribute("selection");
    if (!select) return null;
    if (select.length > 0) {
        linkText = select;
        if (!linkText) return; /* no selection */

        var tResult1 = select.match(/include=[\d]*/);
        var id = null;
        if (tResult1) {
            for (var i = 0; i < tResult1.length; i++) {
                tResult1[i] = tResult1[i].replace("include=", "");
                id = tResult1[i];
            }
        }

        var retStr = "";
        if (id) retStr += "&id=" + id;
        var currQuery = location.search.substring(1);
        location.href += (currQuery ? "&" : "?") + "input=true" + retStr;
    }
}

function Cancel() {
    tinyMCEPopup.close();
}

function Select(val) {
    tinyMCE.activeEditor.selection.setContent(val);
    tinyMCEPopup.close();
}