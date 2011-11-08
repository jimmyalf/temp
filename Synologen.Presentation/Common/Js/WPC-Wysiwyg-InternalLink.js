
function getRadWindow(){
	if (window.radWindow) return window.radWindow;
	if (window.frameElement && window.frameElement.radWindow) return window.frameElement.radWindow;
	return null;
}
function initDialog(){ //Can this method be removed?
   var clientParameters = getRadWindow().ClientParameters;
 

}

if (window.attachEvent){
   window.attachEvent("onload", initDialog);
}
else if (window.addEventListener){
   window.addEventListener("load", initDialog, false);
}

function getSafeAttribute(element,strAttribute){
    if (element && element.getAttribute && element.getAttribute(strAttribute) && element.getAttribute(strAttribute).length>0){
        return element.getAttribute(strAttribute);
    }
    else return null;
}
            					
function GetInputValue(){
	var args = getRadWindow().ClientParameters;
	if (!args) return null;
	if (!args.selected) return null;
    var select = args.selected;
	var id = null;
	var linkAttr = getSafeAttribute(select,"href");
	if (linkAttr){
	   var tResult1 = linkAttr.match (/internal=[\d]*/);
	   if (tResult1) {
		   for (var i = 0; i < tResult1.length; i++) {
				tResult1[i] = tResult1[i].replace ("internal=", "");
				id = tResult1[i];
		   }
	   }
	}
	if(select && select.innerHTML){ 
		text = select.innerHTML;
	}
	else{
		if(select && select.length>0) text = select;
		else text = "";
	}

	var title = getSafeAttribute(select,"title");
	var target = getSafeAttribute(select,"target");
	var width = getSafeAttribute(select,"width");
	var height = getSafeAttribute(select,"height");
	var left = getSafeAttribute(select,"left");
	var top = getSafeAttribute(select,"top");
	var retStr = "";
	if (id) retStr += "&il-id=" + id;
	if (text) retStr += "&il-text=" + encodeURIComponent(text);
	if (title) retStr += "&il-title=" + encodeURIComponent(title);
	if (target) retStr += "&il-target=" + target;
	if (width) retStr += "&il-width=" + width;
	if (height) retStr += "&il-height=" + height;
	if (left) retStr += "&il-left=" + left;
	if (top)  retStr += "&il-top=" + top;
	var currQuery = location.search.substring(1);
	location.href += (currQuery ? "&" : "?") + "input=true" +  retStr;
}
            
function Cancel () {
	var closeArgument = {};
	closeArgument.operation = "cancel";
	getRadWindow().close(closeArgument);
	self.close ();
}
    
function Select (val){
	var closeArgument = {};
	closeArgument.operation = "select";
	closeArgument.value = val;
	getRadWindow().close(closeArgument);
	self.close ();
}
   
function Remove () {		
	var closeArgument = {};
	closeArgument.operation = "remove";
	getRadWindow().close(closeArgument);
	self.close ();
}

function ShowFirstPage(){
	var firstPage = document.getElementById("first-page");
	var secondPage = document.getElementById("second-page");
	firstPage.style.display = 'block';
	firstPage.style.visibility='visible';
	secondPage.style.display = 'none';
	secondPage.style.visibility='hidden';
}

function ShowSecondPage(){
	var firstPage = document.getElementById("first-page");
	var secondPage = document.getElementById("second-page");
	firstPage.style.display = 'none';
	firstPage.style.visibility='hidden';
	secondPage.style.display = 'block';
	secondPage.style.visibility='visible';
}