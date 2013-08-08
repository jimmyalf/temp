WPC.UI = function() {
	return {
		// =init
		init : function() {
			// Add UI enhancing functions here
			WPC.UI.enhanceUI();
			WPC.addEvent(window, 'resize', WPC.UI.refreshUI);
			var forms = document.getElementsByTagName("form");
			if (forms) {
				for (var i=0;i<forms.length;i++) {
					WPC.addEvent(forms[i], 'submit', WPC.UI.refreshUI);
				}
			}
		},
		
		// =enhanceUI
		enhanceUI : function(isRecursiceCall) {
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
								WPC.UI.enhanceUI(true);
							}
							
							contentElement.style.overflow = 'hidden';

							if (sidebarElement)
								sidebarElement.style.height = contentElement.offsetHeight + 'px';
						}
					}
				}
			}
		},
		
		// =refreshUI
		refreshUI : function()
		{
			WPC.UI.enhanceUI(false);
		},
		
		// =stripeTables
		stripeTables : function() {
		  if (document.getElementsByTagName) {
			var tables = document.getElementsByTagName("table");
			for (var i=0; i<tables.length; i++) {
			  if (tables[i].className.match("striped"))
			  {
				var rows = tables[i].getElementsByTagName("tr");
				for (var j=0; j<rows.length; j=j+2) {
				  if (!rows[j].className.match("header")) {
					WPC.addClass(rows[j],"odd");
				  }
				}
			  }
			}
		  }
		},

		// =stripeDefinitionLists
		stripeDefinitionLists : function() {
		  if (document.getElementsByTagName) {
			var deflist = document.getElementsByTagName("dl");
			for (var i=0; i<deflist.length; i++) {
			  if (deflist[i].className.match("striped"))
			  {
				var dt = deflist[i].getElementsByTagName("dt");
				for (var j=0; j<dt.length; j=j+2) {
					WPC.addClass(dt[j],"odd");
				}
				var dd = deflist[i].getElementsByTagName("dd");
				for (var j=0; j<dd.length; j=j+2) {
					WPC.addClass(dd[j],"odd");
				}
			  }
			}
		  }
		}
	};
}();