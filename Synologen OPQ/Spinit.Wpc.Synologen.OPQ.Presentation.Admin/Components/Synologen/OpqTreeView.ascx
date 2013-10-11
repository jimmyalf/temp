<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqTreeView.ascx.cs"
	Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.OpqTreeView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:PlaceHolder ID="phScriptManager" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript">
	//<!--
	function onClientContextMenuShowing(sender, args) {
		var treeNode = args.get_node();
		treeNode.set_selected(true);
		//enable/disable menu items
		setMenuItemsState(args.get_menu().get_items(), treeNode);
	}

	function onClientContextMenuItemClicking(sender, args) {
		var menuItem = args.get_menuItem();
		var treeNode = args.get_node();
		menuItem.get_menu().hide();

		switch (menuItem.get_value()) {
			case "Rename":
				treeNode.startEdit();
				break;
			case "NewMenu":
				break;
			case "NewRoutine":
				break;
			case "Delete":
				var result = confirm("Är du säker på att du vill ta bort noden: " + treeNode.get_text());
				args.set_cancel(!result);
				break;
		}
	}

	//this method disables the appropriate context menu items
	function setMenuItemsState(menuItems, treeNode) {
		for (var i = 0; i < menuItems.get_count(); i++) {
			var menuItem = menuItems.getItem(i);
			switch (menuItem.get_value()) {
				case "Rename":
					formatMenuItem(menuItem, treeNode, 'Byt namn "{0}"');
					break;
				case "NewRoutine":
					var nodeValue = treeNode.get_value();
					if (nodeValue && nodeValue.indexOf("Menu") == -1) {
						menuItem.set_enabled(false);
					}
					else {
						menuItem.set_enabled(true);
					}
					break;
				case "NewMenu":
					var nodeValue = treeNode.get_value();
					if (nodeValue && nodeValue.indexOf("Menu") == -1) {
						menuItem.set_enabled(false);
					}
					else {
						menuItem.set_enabled(true);
					}
					break;
			}
		}
	}

	//formats the Text of the menu item
	function formatMenuItem(menuItem, treeNode, formatString) {
		var nodeValue = treeNode.get_value();
		var newText = String.format(formatString, treeNode.get_text());
		menuItem.set_text(newText);
	}

	//-->
</script>

<telerik:RadTreeView 
	ID="rtvNodes" 
	EnableDragAndDrop="true" 
	EnableDragAndDropBetweenNodes="true"
	AllowNodeEditing="true"
	OnContextMenuItemClick="rtvNodes_ContextMenuItemClick"
	OnNodeDrop="rtvNodes_NodeDrop" 
	OnNodeEdit="rtvNodes_NodeEdit"
	OnNodeClick="rtvNodes_Click"
	OnClientContextMenuItemClicking="onClientContextMenuItemClicking"
    OnClientContextMenuShowing="onClientContextMenuShowing"
	runat="server">
	<ContextMenus>
		<telerik:RadTreeViewContextMenu ID="RadTreeViewContextMenu2" runat="server">
			<Items>
				<telerik:RadMenuItem Value="NewMenu" Text="Lägg till meny" ImageUrl="~/Common/Icons/Menu.png">
				</telerik:RadMenuItem>
				<telerik:RadMenuItem Value="NewRoutine" Text="Lägg till rutin" ImageUrl="~/Common/Icons/Page.png">
				</telerik:RadMenuItem>
				<telerik:RadMenuItem IsSeparator="true" />
				<telerik:RadMenuItem Value="Delete" Text="Ta bort" ImageUrl="~/Common/Icons/Trash.png">
				</telerik:RadMenuItem>				
			</Items>
			<CollapseAnimation Type="none" />
		</telerik:RadTreeViewContextMenu>
	</ContextMenus>
</telerik:RadTreeView>
