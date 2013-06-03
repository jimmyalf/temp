/*=====================================
	JavaScript methods used for
	enhancing Internet Explorer 7-
=====================================*/
WPC.Browser.IE = function() {
	// Private
	function clearingElement() {
		var newElement = document.createElement("span");
		with(newElement) {
			style.width = "100%";
			style.height = "0";
			style.clear = "both";
		}
		return newElement;
	}

	// Public
    return {
		// =init
		init : function() {
			WPC.Browser.IE.addClassToInput("text", "txt");
			WPC.Browser.IE.addClassToInput("password", "txt");
			
			// Add clear fix
			var elements = WPC.Util.getElementsByClass("Index.aspx", document, "div");
			if (elements.length < 1)
				WPC.Browser.IE.fixClearingByClass("clearLeft", document, "div");
		},
		
		// =addClassToInput
		addClassToInput : function (type, className)
		{
			var inputCollection = document.getElementsByTagName("input");
			for (i=0; i<inputCollection.length; i++) {
				if (inputCollection[i].attributes["type"].value == type) {
					WPC.addClass(inputCollection[i], className);
				}
			}
		},
		
		// =fixClearingByClass
		fixClearingByClass : function (className, startingNode, filterByElement) {
			// Get the parent element
			var eCollection = WPC.Util.getElementsByClass(className, startingNode, filterByElement);

			// For each element
			for (i=0; i<eCollection.length; i++) {
				// Add an element to the root node
				var rootNode = eCollection[i].parentNode;
				rootNode.insertBefore(clearingElement(), eCollection[i]);
				
			}
		}
	};
}();

WPC.addEvent(window, 'load', WPC.Browser.IE.init);