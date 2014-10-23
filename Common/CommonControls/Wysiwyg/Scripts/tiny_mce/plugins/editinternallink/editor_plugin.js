(function() {
	tinymce.create('tinymce.plugins.EditInternalLink', {
		init : function(ed, url) {
			
			ed.addCommand('addInternalLink', function() {
				ed.windowManager.open({
					file: '/Content/internallink.aspx',
					width: 720,
					height: 520,
					inline: 1,
					title: 'Internal link'
				}, {
					selection: ed.selection.getContent()
				});
			});
			
			ed.addCommand('removeInternalLink', function() {
				ed.execCommand("unlink", true);
			});
			
			ed.addButton('addinternallink', {
				title: 'Add internal link',
				cmd: 'addInternalLink',
				image: '/CommonResources/CommonControls/Wysiwyg/img/InternalLink.gif'
			});
			
			ed.addButton('removeinternallink', {
				title: 'Remove internal link',
				cmd: 'removeInternalLink',
				image: '/CommonResources/CommonControls/Wysiwyg/img/RemoveInternalLink.gif'
			});
			
			ed.onNodeChange.add(function(ed, cm, n) {
				
				var linkNode = n.nodeName == 'A' ? n : ed.dom.getParent(n, function(node) { return node.nodeName == 'A' });
				
				if (linkNode)
				{
					if (linkNode.href.indexOf('#internal=') > -1)
					{
						cm.setDisabled('removeinternallink', false);
						cm.setActive('addinternallink', true);
						return;
					}

					cm.setDisabled('addinternallink', linkNode.href.indexOf('#include=') > -1);
				}
				else
				{
					cm.setDisabled('addinternallink', false);
				}
				
				cm.setDisabled('removeinternallink', true);
				cm.setActive('addinternallink', false);
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
	tinymce.PluginManager.add('editinternallink', tinymce.plugins.EditInternalLink);
})();