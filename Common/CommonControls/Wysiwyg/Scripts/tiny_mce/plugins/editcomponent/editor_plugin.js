(function() {
	tinymce.create('tinymce.plugins.EditComponent', {
		init : function(ed, url) {
			
			ed.addCommand('addComponent', function() {
				ed.windowManager.open({
					file: '/Wysiwyg/Components',
					width: 640,
					height: 480,
					inline: 1
				}, {
					selection: ed.selection.getContent()
				});
			});
			
			ed.addCommand('removeComponent', function() {
				var n = ed.selection.getNode();
				ed.dom.remove(n);
			});
			
			ed.addButton('addcomponent', {
				title: 'Add component',
				cmd: 'addComponent',
				image: '/CommonResources/CommonControls/Wysiwyg/img/Component.gif'
			});
			
			ed.addButton('removecomponent', {
				title: 'Remove component',
				cmd: 'removeComponent',
				image: '/CommonResources/CommonControls/Wysiwyg/img/RemoveComponent.gif'
			});
			
			ed.onNodeChange.add(function(ed, cm, n) {				
				var linkNode = n.nodeName == 'A' ? n : ed.dom.getParent(n, function(node) { return node.nodeName == 'A' });
				
				if (linkNode)
				{
					if (linkNode.href.indexOf('#component=') > -1)
					{
						cm.setDisabled('removecomponent', false);
						cm.setActive('addcomponent', true);
						cm.setDisabled('addcomponent', true);
						return;
					}
				}
				
				cm.setDisabled('removecomponent', true);
				cm.setActive('addcomponent', false);
				cm.setDisabled('addcomponent', false);
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
	tinymce.PluginManager.add('editcomponent', tinymce.plugins.EditComponent);
})();