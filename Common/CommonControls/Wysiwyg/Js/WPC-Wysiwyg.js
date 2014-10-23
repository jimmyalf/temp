var theEditor = null;
var selected = '';
var selectedElement = null;


/* GENERAL */

function removeThisNode(node) {
    if (node && node.parentNode) {
        node.parentNode.removeChild(node);
    }
}

function getOuterHtml(node) {
    //For IE
    if (node.outerHTML) return node.outerHTML;
    else { //For FF
        var thisNode = node.cloneNode(true);
        var newDiv = document.createElement("div");
        newDiv.appendChild(thisNode);
        return newDiv.innerHTML;
    }
}

function matchAElement(element, counter, counterLimit, classString) {
    if (counter >= counterLimit || !element) return null; //if at limit or node is not valid
    counter = counter + 1;
    if (!element.parentNode) return null; //if no parent
    if (element.nodeName.toLowerCase() !== "a") return (matchAElement(element.parentNode, counter, counterLimit, classString)); //goto parent
    var cls = element.getAttribute("class"); //works in FF
    if (cls && cls.toString() == classString) return element; //match found
    var cls = element.getAttribute("className"); //works in IE
    if (cls && cls.toString() == classString) return element; //match found
    else return matchAElement(element.parentNode, counter++, counterLimit); //goto parent
}

function copySafeAttribute(fromElement, strAttributeName, toElement) {
    if (fromElement && fromElement.getAttribute && fromElement.getAttribute(strAttributeName)) {
        if (toElement && toElement.getAttribute && toElement.getAttribute(strAttributeName)) {
            toElement.setAttribute(strAttributeName, fromElement.getAttribute(strAttributeName));
        }
    }
}
