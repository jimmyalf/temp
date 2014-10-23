WPC.Util = function() {
	return {
		// =getElementsByClass
		getElementsByClass : function(searchClass,node,tag) {
			var classElements = new Array();
			if ( node == null )
				node = document;
			if ( tag == null )
				tag = '*';
			var els = node.getElementsByTagName(tag);
			var elsLen = els.length;
			var pattern = new RegExp("(^|\\s)"+searchClass+"(\\s|$)");
			for (i = 0, j = 0; i < elsLen; i++) {
				if ( pattern.test(els[i].className) ) {
					classElements[j] = els[i];
					j++;
				}
			}
			return classElements;
		},
		
		// =makeEqualHeight
		makeEqualHeight : function (element1, element2)
		{
			if (element1 && element2)
			{
				if (element1.offsetHeight <  element2.offsetHeight)
					element1.style.height = element2.offsetHeight + "px";
					
				if (element2.offsetHeight < element1.offsetHeight)
					element2.style.height = element1.offsetHeight + "px";
			}
		},
		
		// =getViewportHeight
		getViewportHeight : function () {
			var viewportHeight = 0;
			if (typeof(window.innerHeight) == 'number') {
				viewportHeight = window.innerHeight;
			}
			else {
				if (document.documentElement && document.documentElement.clientHeight) {
					viewportHeight = document.documentElement.clientHeight;
				}
				else {
					if (document.body && document.body.clientHeight) {
						viewportHeight = document.body.clientHeight;
					}
				}
			}
			return viewportHeight;
		}
	};
}();