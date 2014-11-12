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

function getSafeAttribute(element, strAttribute) {
    if (element && element.getAttribute && element.getAttribute(strAttribute) && element.getAttribute(strAttribute).length > 0) {
        return element.getAttribute(strAttribute);
    }
    else return null;
}

function getAttribute(attr) {
    return tinyMCEPopup.getWindowArg(attr);
}

function GetInputValue() {
    var select = getAttribute("selection");
    if (!select) return null;
    var id = null;
    var text = "";
    var linkAttr = getSafeAttribute(select, "href");
    if (linkAttr) {
        var tResult1 = linkAttr.match(/internal=[\d]*/);
        if (tResult1) {
            for (var i = 0; i < tResult1.length; i++) {
                tResult1[i] = tResult1[i].replace("internal=", "");
                id = tResult1[i];
            }
        }
    }
    if (select && select.innerHTML) {
        text = select.innerHTML;
    }
    else {
        if (select && select.length > 0) text = select;
        else text = "";
    }

    var title = getSafeAttribute(select, "title");
    var target = getSafeAttribute(select, "target");
    var width = getSafeAttribute(select, "width");
    var height = getSafeAttribute(select, "height");
    var left = getSafeAttribute(select, "left");
    var top = getSafeAttribute(select, "top");
    var retStr = "";
    if (id) retStr += "&il-id=" + id;
    if (text) retStr += "&il-text=" + encodeURIComponent(text);
    if (title) retStr += "&il-title=" + encodeURIComponent(title);
    if (target) retStr += "&il-target=" + target;
    if (width) retStr += "&il-width=" + width;
    if (height) retStr += "&il-height=" + height;
    if (left) retStr += "&il-left=" + left;
    if (top) retStr += "&il-top=" + top;
    var currQuery = location.search.substring(1);
    location.href += (currQuery ? "&" : "?") + "input=true" + retStr;
}

function Cancel() {
    tinyMCEPopup.close();
}

function Select(val) {
    tinyMCE.activeEditor.selection.setContent(val);
    tinyMCEPopup.close();
}