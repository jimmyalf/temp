/* =Namespaces for WPC */
var WPC = {
    Browser : {
			IE : {}
    },
    UI : {},
    Util : {}
};

WPC = function() {
	return {
		// =addEvent
		addEvent : function (obj, evType, fn){ 
			if (obj.addEventListener){ 
				obj.addEventListener(evType, fn, false); 
				return true; 
			} else if (obj.attachEvent){ 
				var r = obj.attachEvent("on"+evType, fn); 
				return r; 
			} else { 
				return false; 
			} 
		},
		
		// =addClass
		addClass : function (element,name) {
			if (!element.className) {
				element.className = name;
			} else {
				element.className+= " ";
				element.className+= name;
			}
		},
		
		// =addClassByType
		addClassByType : function (elementType, className)
		{
			var eCollection = document.getElementsByTagName(elementType);
			for (i=0; i<eCollection.length; i++) {
				WPC.addClass(eCollection[i], className);
			}
		}
	};
}();

WPC.Browser = function() {
	return {
	
	};
}();