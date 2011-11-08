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
       if (!linkText) {
         return; // no selection
       }
       var tRegExp = new RegExp("componentid=[\\w]+", "i");
       var tResult = select.match (tRegExp);
       var target = null;
       if (tResult) {
           for (var i = 0; i < tResult.length; i++) {
                var ttRegExp = new RegExp("componentid=", "i");
                tResult[i] = tResult[i].replace (ttRegExp, "");
                target = tResult[i];
           }
           //var tRegExpStr = new RegExp("\\?[&=;#\\d\\w]+", "i");
           var tRegExpStr = new RegExp("\\?[^\"]+", "i");
           var tResultStr = select.match (tRegExpStr);
           var cmpStr = tResultStr[0];
           cmpStr = cmpStr.replace(/&/g, "*am*");
           cmpStr = cmpStr.replace(/=/g, "*eq*");
           //cmpStr = cmpStr.replace(/\+/g, "*pl*");
           cmpStr = cmpStr.replace(/#/g, "*br*");
           cmpStr = cmpStr.replace(/\?/g, "");
           
           var currQuery = location.search.substring(1);
           location.href += (currQuery ? "&" : "?") + "input=true"
                            + "&component=" + target
                            + "&componentstring=" + cmpStr;
       }
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

function getDocHeight(doc) {
    var docHt = 0, sh, oh;
    if (doc.height) docHt = doc.height;
    else if (doc.body) {
       if (doc.body.scrollHeight) docHt = sh = doc.body.scrollHeight;
        if (doc.body.offsetHeight) docHt = oh = doc.body.offsetHeight;
        if (sh && oh) docHt = Math.max(sh, oh);
    }
    var wrapper = doc.getElementById('wrapper')
    if (wrapper)
        docHt = wrapper.scrollHeight + 10;
    return docHt;
}


function setIframeHeight(iframeName) {
    var iframeWin = window.frames[iframeName];
    var iframeEl = document.getElementById? document.getElementById(iframeName): document.all? document.all[iframeName]: null;
    if ( iframeEl && iframeWin ) {
        iframeEl.style.height = "auto";
        var docHt = getDocHeight(iframeWin.document);
        if (docHt) iframeEl.style.height = docHt + 30 + "px";
    }
}

var timer;
var openMenu = function() {
	clearTimeout(timer);
	$("#components-container").animate({ left: '0' }, 250, 'swing').attr("class", "open");
}
var closeMenu = function() {
	timer = setTimeout(function() {
		$("#components-container").animate({ left: '-150' }, 250, 'swing').attr("class", "");
	}, 150);
}

var initializeHelpers = function() {
	$(".description").hide();
	$("input").focus(function() {
		$(this).parent(".form-item").find(".description").show();
		$(this).closest(".truefalse").find(".description").show();
	});
	$("input").blur(function() {
		$(this).parent(".form-item").find(".description").hide();
		$(this).closest(".truefalse").find(".description").hide();
	});
}

var initAddComponent = function() {
	initializeHelpers();
	$("form").validate();
	$("form input:first").focus();
}

var initMenu = function() {
	$("#components-container #handle").click(function() {
		if ($("#components-container").hasClass("open")) {
			closeMenu();
		} else {
			openMenu();
		}
	});
	$("#components-container a").focus(openMenu);
	$("#components-container a").blur(closeMenu);
	$("#components-container a").click(function(e) {
		e.preventDefault();
		closeMenu();
		if ($("#components-form").length < 1) {
			$("#components-container").after("<div id='components-form'/>");
		}
		var url = $(this).attr("href");
		$.ajax({
			url: url,
			success: function(data) {
				var form = $(data).find("form")
				if (form.length != 1) {
					document.location.href = url;
				}
				$("#components-form").html(form);
				initAddComponent();
			},
			error: function() {
				document.location.href = url;
			}
		})
	});
}

var setHeight = function() {
	var height = $(window).height();
	$("#components-container").height(height);
	$("#handle").height(height);
}

$().ready(function() {
	if ($("body").hasClass("close")) {
		Select($("#returnvalue").val());
	} else {
		$("html").attr("class", "js");
		$("ul.components").wrap($("<div id='components-container'>")).before("<div id='handle'>Open/Close</div>");
		$("#components-index #components-container").attr("class", "open");
		setHeight();
		initMenu();
		initAddComponent();
	}
});