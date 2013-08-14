function initWPC()
{
	// Force elements to have equal in height;
	/* = Not used at the moment
	var mainElement = document.getElementById('dCompMainColumn');
	var sidebarElement = document.getElementById('dCompSidebar');
	WPC.Util.makeEqualHeight(mainElement, sidebarElement);
	*/
	
	if (!WPC.Util.getElementsByClass("Log-Logs-aspx", null, "div")[0]) {
		WPC.UI.init();
	}
}

WPC.addEvent(window, 'load', initWPC);
WPC.addEvent(window, 'load', WPC.UI.stripeTables);
WPC.addEvent(window, 'load', WPC.UI.stripeDefinitionLists);

$(document).ready(function() {
	$.ajaxSetup({
		// Disable caching of AJAX responses */
		cache: false
	});

	$("#dAppLocation a.change-location").click(function(e) {
		e.preventDefault();
		var url = $("#dAppLocation a.change-location").attr("href");
		$("#dAppLocation a.change-location").after("<ul id='ajax-locations'/>");
		$("ul#ajax-locations").load(url + " ul#locations li");
	});
});