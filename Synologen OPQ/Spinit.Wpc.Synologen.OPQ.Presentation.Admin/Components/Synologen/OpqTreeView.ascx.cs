using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Presentation;
using Telerik.Web.UI;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	/// <summary>
	/// Node control
	/// </summary>
	public partial class OpqTreeView : Presentation.OpqControlPage
	{
		#region Page_Load & Init

		protected enum NodeAction
		{
			NotSet = 0,
			NewMenu = 1,
			NewRoutine = 2,
			Rename = 3,
			Delete = 4
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				InitializeUnit();
				if (!Page.IsPostBack)
				{
					BuildTree();
				}

			}
			catch (BaseCodeException ex)
			{
				ExceptionHandler.HandleException(Page, ex);
			}
		}

		private void InitializeUnit()
		{
			//check to see if we have a scriptmanager
			ScriptManager sm = ScriptManager.GetCurrent(Page);
			if (sm == null)
			{
				sm = new ScriptManager();
				sm.EnablePartialRendering = true;
				phScriptManager.Controls.Add(sm);
			}
		}

		#endregion

		#region Tree Builder

		private void BuildTree()
		{
			rtvNodes.Nodes.Clear();
			var bNode = new BNode(_context);
			var nodes = bNode.GetNodes(null, null, true, false, false);
			foreach (var node in nodes)
			{
				var treeNode = new RadTreeNode(node.Name);
				SetTreeNodeValue(treeNode, node, node.IsMenu);
				SetTreeNodeImage(treeNode, node, node.IsMenu);
				rtvNodes.Nodes.Add(treeNode);
				BuildLeaf(treeNode, node.Id);
			}
		}

		private void BuildLeaf(RadTreeNode radNode, int nodeId)
		{
			var bNode = new BNode(_context);
			var nodes = bNode.GetAllChilds(nodeId, true, false);
			foreach (var node in nodes)
			{
				var treeNode = new RadTreeNode(node.Name);
				SetTreeNodeValue(treeNode, node, node.IsMenu);
				SetTreeNodeImage(treeNode, node, node.IsMenu);
				radNode.Nodes.Add(treeNode);
				if ((node.Childs != null) && (node.Childs.Count > 0))
				{
					BuildLeafFromList(treeNode, node.Childs);
				}
				if (node.Id == _nodeId)
				{
					treeNode.Selected = true;
					treeNode.ExpandParentNodes();
				}
			}
		}

		private void BuildLeafFromList(RadTreeNode radNode, List<Node> childs)
		{
			foreach (var node in childs)
			{
				var treeNode = new RadTreeNode(node.Name);
				SetTreeNodeValue(treeNode, node, node.IsMenu);
				SetTreeNodeImage(treeNode, node, node.IsMenu);
				radNode.Nodes.Add(treeNode);
				if ((node.Childs != null) && (node.Childs.Count > 0))
				{
					BuildLeafFromList(treeNode, node.Childs);
				}
				if (node.Id == _nodeId)
				{
					treeNode.Selected = true;
					treeNode.ExpandParentNodes();
				}
			}
		}

		#endregion

		#region Control Events

		protected void rtvNodes_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
		{
			RadTreeNode clickedNode = e.Node;
			var action = NodeAction.NotSet;
			try
			{
				action = (NodeAction)Enum.Parse(typeof(NodeAction), e.MenuItem.Value);
			}
			catch {}

			switch (action)
			{
				case NodeAction.NewMenu:
					NewMenu(clickedNode);
					break;
				case NodeAction.NewRoutine:
					NewRoutine(clickedNode);
					break;
				case NodeAction.Rename:
					break;
				case NodeAction.Delete:
					DeleteNode(clickedNode);
					break;
			}
		}

		protected void rtvNodes_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
		{
			RadTreeNode sourceDragNode = e.SourceDragNode;
			RadTreeNode destDragNode = e.DestDragNode;
			if (sourceDragNode == null || destDragNode == null || sourceDragNode.Equals(destDragNode) || sourceDragNode.IsAncestorOf(destDragNode))
			{
				return;
			}
			int sourceNodeId = GetNodeIdFromTreeNode(sourceDragNode);
			int destNodeId = GetNodeIdFromTreeNode(destDragNode);
			MoveNode(e.DropPosition, sourceNodeId, destNodeId);

		}

		protected void rtvNodes_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
		{
			EditNode(e.Node, e.Text);
		}

		protected void rtvNodes_Click(object sender, RadTreeNodeEventArgs e)
		{
			ClickNode(e.Node);
		}

		#endregion

		#region Actions

		private void MoveNode(RadTreeViewDropPosition dropPosition, int sourceNodeId, int destNodeId)
		{
			if (sourceNodeId <= 0) return;
			if (destNodeId <= 0) return;
			var bNode = new BNode(_context);
			Node sourceNode = bNode.GetNode(sourceNodeId, true);
			Node destNode = bNode.GetNode(destNodeId, true);
			bool success = false;

			switch (dropPosition)
			{
				case RadTreeViewDropPosition.Over:
					success = HandleDropOver(sourceNode, destNode);
					break;
				case RadTreeViewDropPosition.Above:
					success = HandleDropAbove(sourceNode, destNode);
					break;
				case RadTreeViewDropPosition.Below:
					success = HandleDropBelow(sourceNode, destNode);
					break;
			}
			if (success)
				Response.Redirect(string.Format(ComponentPages.OpqStartQueryNode, sourceNodeId));				
		}

		private bool HandleDropBelow(Node sourceNode, Node destNode)
		{
			if ((sourceNode == null) || (destNode == null))
				return false;
			var bNode = new BNode(_context);
			bNode.MoveNode(NodeMoveActions.MoveAfter, sourceNode.Id, destNode.Id);
			return true;
		}

		private bool HandleDropAbove(Node sourceNode, Node destNode)
		{
			if ((sourceNode == null) || (destNode == null))
				return false;
			var bNode = new BNode(_context);
			bNode.MoveNode(NodeMoveActions.MoveBefore, sourceNode.Id, destNode.Id);
			return true;
		}

		private bool HandleDropOver(Node sourceNode, Node destNode)
		{
			if ((sourceNode == null) || (destNode == null))
				return false;
			//If source node is menu and destination page -> illegal move
			if (sourceNode.IsMenu && !destNode.IsMenu)
			{
				ShowNegativeFeedBack("MenuToPageIllegalException");
				return false;
			}

			//If source node is page and destination page -> illegal move
			if (!sourceNode.IsMenu && !destNode.IsMenu)
			{
				ShowNegativeFeedBack("PageToPageIllegalException");
				return false;
			}
			var bNode = new BNode(_context);
			bNode.MoveNode(NodeMoveActions.MoveInto, sourceNode.Id, destNode.Id);
			return true;
		}

		private void EditNode(RadTreeNode treeNode, string text)
		{
			treeNode.Text = text;
			var bNode = new BNode(_context);
			int nodeId = GetNodeIdFromTreeNode(treeNode);
			if (nodeId <= 0) return;
			var node = bNode.GetNode(nodeId, false);
			bNode.ChangeNode(nodeId, node.Parent, text);
			bNode.Publish(nodeId);
			bNode.UnLock(nodeId);
			if (!IsMenu(treeNode))
			{
				Response.Redirect(string.Concat(ComponentPages.OpqStart, "?nodeId=", nodeId));
			}
		}

		private void NewMenu(RadTreeNode clickedNode)
		{
			string name = string.Format("Ny mapp {0}", clickedNode.Nodes.Count + 1);
			var bNode = new BNode(_context);
			Node node = bNode.CreateNode(GetNodeIdFromTreeNode(clickedNode), name, true);
			var newFolder = new RadTreeNode(name);
			newFolder.Selected = true;
			SetTreeNodeImage(newFolder, node, true);
			SetTreeNodeValue(newFolder, node, true);
			clickedNode.Nodes.Add(newFolder);
			clickedNode.Expanded = true;
			clickedNode.Font.Bold = true;
			StartNodeInEditMode(newFolder.Value);
		}

		private void NewRoutine(RadTreeNode clickedNode)
		{
			string name = string.Format("Ny rutin {0}", clickedNode.Nodes.Count + 1);
			var bNode = new BNode(_context);
			Node node = bNode.CreateNode(GetNodeIdFromTreeNode(clickedNode), name, false);
			var newFolder = new RadTreeNode(name);
			newFolder.Selected = true;
			newFolder.ImageUrl = clickedNode.ImageUrl;
			SetTreeNodeImage(newFolder, node, false);
			SetTreeNodeValue(newFolder, node, false);
			clickedNode.Nodes.Add(newFolder);
			clickedNode.Expanded = true;
			clickedNode.Font.Bold = true;
			StartNodeInEditMode(newFolder.Value);
		}

		private void DeleteNode(RadTreeNode clickedNode)
		{
			clickedNode.Remove();
			int nodeId = GetNodeIdFromTreeNode(clickedNode);
			var bNode = new BNode(_context);
			bNode.DeleteNode(nodeId, false);
		}

		private void ClickNode(RadTreeNode clickedNode)
		{
			int nodeId = GetNodeIdFromTreeNode(clickedNode);
			if (nodeId <= 0) return;
			Response.Redirect(string.Concat(ComponentPages.OpqStart, "?nodeId=", nodeId));
		}

		#endregion

		#region Help methods

		private void StartNodeInEditMode(string nodeValue)
		{
			//find the node by its Value and edit it when page loads
			string js = "Sys.Application.add_load(editNode); function editNode(){ ";
			js += "var tree = $find(\"" + rtvNodes.ClientID + "\");";
			js += "var node = tree.findNodeByValue('" + nodeValue + "');";
			js += "if (node) node.startEdit();";
			js += "Sys.Application.remove_load(editNode);};";

			RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "nodeEdit", js, true);
		}

		private static int GetNodeIdFromTreeNode(RadTreeNode node)
		{
			int nodeValue;
			if (node == null) return 0;
			string idPart = node.Value.Substring(0, node.Value.IndexOf("_"));
			int.TryParse(idPart, out nodeValue);
			return nodeValue;
		}

		private static bool IsMenu(RadTreeNode node)
		{
			if (node == null) return false;
			string menuPart = node.Value.Substring(node.Value.IndexOf("_")+1);
			return menuPart == "Menu";
		}
		
		private static void SetTreeNodeValue(RadTreeNode treeNode, Node node, bool isMenu)
		{
			treeNode.Value = isMenu
			                 	?
			                 		string.Concat(node.Id, "_", "Menu")
			                 	:
			                 		string.Concat(node.Id, "_", "Page"); 
		}

		private void SetTreeNodeImage(RadTreeNode treeNode, Node node, bool isMenu)
		{
			treeNode.ImageUrl = isMenu
			                    	?
			                    		"~/Common/Icons/Menu.png"
			                    	:
			                    		"~/Common/Icons/Page.png";
		}


		#endregion

	}
}