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
            
function GetInputValue(){
	var args = getRadWindow().ClientParameters;
	if (!args) return null;
	if (!args.selected) return null;
    var select = args.selected;
    if (select.length > 0) {
       linkText = select;
       if (!linkText) return; /* no selection */

       var tResult1 = select.match (/include=[\d]*/);
       var id = null;
       if (tResult1) {
           for (var i = 0; i < tResult1.length; i++) {
                tResult1[i] = tResult1[i].replace ("include=", "");
                id = tResult1[i];
           }
       }
       
       var retStr = "";
       if (id) retStr += "&id=" + id;
       var currQuery = location.search.substring(1);
       location.href += (currQuery ? "&" : "?") + "input=true" +  retStr;
    }
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