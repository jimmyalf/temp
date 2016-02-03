(function() {
	tinymce.create('tinymce.plugins.EditInclude', {
		init : function(ed, url) {
			
			ed.addCommand('addInclude', function() {
				ed.windowManager.open({
					file: '/Content/include.aspx',
					width: 360,
					height: 520,
					inline: 1
				});
			});
			
			ed.addCommand('removeInclude', function() {
				var n = ed.selection.getNode();
				ed.dom.remove(n);
			});
			
			ed.addButton('addinclude', {
				title: 'Add include',
				cmd: 'addInclude',
				image: '/CommonResources/CommonControls/Wysiwyg/img/Include.gif'
			});
			
			ed.addButton('removeinclude', {
				title: 'Remove include',
				cmd: 'removeInclude',
				image: '/CommonResources/CommonControls/Wysiwyg/img/RemoveInclude.gif'
			});
			
			ed.onNodeChange.add(function(ed, cm, n) {				
				var linkNode = n.nodeName == 'A' ? n : ed.dom.getParent(n, function(node) { return node.nodeName == 'A' });
				
				if (linkNode)
				{
					if (linkNode.href.indexOf('#include=') > -1)
					{
						cm.setDisabled('removeinclude', false);
						cm.setActive('addinclude', true);
						cm.setDisabled('addinclude', true);
						return;
					}
				}
				
				cm.setDisabled('removeinclude', true);
				cm.setActive('addinclude', false);
				cm.setDisabled('addinclude', false);
			});
			
			ed.onClick.add(function(ed, e) {
				e = e.target;

				if (e.nodeName === 'a')
				{
					ed.selection.select(e);
				}
			});
		}
	});
	
	// Register plugin
	tinymce.PluginManager.add('editinclude', tinymce.plugins.EditInclude);
})();