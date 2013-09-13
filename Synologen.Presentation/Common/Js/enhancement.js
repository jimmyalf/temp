function enhanceGUI(isRecursiceCall) {
	if (document.getElementById) {
		var headerElement = document.getElementById('dHeader');
		var contentElement = document.getElementById('dCompContainer'); 
		var footerElement = document.getElementById('dFooter');
		if (headerElement && contentElement && footerElement) {
			var viewportHeight = WPC.Util.getViewportHeight();
			if (viewportHeight > 0) {
				var headerHeight = headerElement.offsetHeight;
				var contentHeight = contentElement.offsetHeight;
				var footerHeight  = footerElement.offsetHeight;
				var maxHeightForContent = viewportHeight - (headerHeight + footerHeight + 1);
				var heightDifference = viewportHeight - (headerHeight + contentHeight + footerHeight + 1);

				var mainElement = document.getElementById('dCompMainColumn');
				var sidebarElement = document.getElementById('dCompSidebar');

				if (heightDifference > 0)
				{
					contentElement.style.height = contentHeight + heightDifference + 'px';
					contentElement.style.overflow = 'hidden';
					if (sidebarElement)
						sidebarElement.style.height = contentElement.offsetHeight + 'px';
				}
				else
				{
					if (mainElement && sidebarElement)
					{
						var compNavElement = document.getElementById('dCompNavigation');
						var compSubNavElement = document.getElementById('dCompSubNavigation');

						var menuOffset = 10;
						if (compNavElement)
							menuOffset += compNavElement.offsetHeight;
						
						if (compSubNavElement)
							menuOffset += compSubNavElement.offsetHeight;
					
						if (sidebarElement.offsetHeight > maxHeightForContent)
							sidebarElement.style.height = 'auto';
							
						if (mainElement.offsetHeight > maxHeightForContent)
							mainElement.style.height = 'auto';
						
						if (contentHeight > maxHeightForContent)
							contentElement.style.height = 'auto';
						
						if ((sidebarElement.offsetHeight + menuOffset) > maxHeightForContent)
							maxHeightForContent = sidebarElement.offsetHeight + menuOffset;
							
						if ((mainElement.offsetHeight + menuOffset) > maxHeightForContent && mainElement.offsetHeight > sidebarElement.offsetHeight)
							maxHeightForContent = mainElement.offsetHeight + menuOffset;
						
						contentElement.style.height = maxHeightForContent + 'px';
					}
					else
					{
						if (isRecursiceCall == true)
							return;

						contentElement.style.height = 'auto';
						enhanceGUI(true);
					}
					
					contentElement.style.overflow = 'hidden';

					if (sidebarElement)
						sidebarElement.style.height = contentElement.offsetHeight + 'px';
				}
			}
		}
	}
}

function makeEqualHeight()
{
	// Force elements to have equal in height;
	var mainElement = document.getElementById('dCompMainColumn');
	var sidebarElement = document.getElementById('dCompSidebar');
	WPC.Util.makeEqualHeight(mainElement, sidebarElement);
}

WPC.Browser.Enhancement = function() {
	return {
		refreshGUI : function ()
		{
			enhanceGUI(false);
		}
	};
}();

function initEnhancement()
{
	// Add GUI enhancing functions here
}

addEvent(window, 'load', initEnhancement);
addEvent(window, 'load', enhanceGUI);
addEvent(window, 'resize', enhanceGUI);
//addEvent(window, 'load', makeEqualHeight);