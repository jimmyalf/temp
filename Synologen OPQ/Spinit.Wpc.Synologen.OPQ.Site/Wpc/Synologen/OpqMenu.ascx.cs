using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Site.Code;
using TreeNode=Spinit.Wpc.Synologen.OPQ.Site.Code.TreeNode;

namespace Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen
{
	public partial class OpqMenu : OpqControlPage
	{
		public enum DisplayType
		{
			FromRoot = 0,
			FromParent = 1,
		}

		private string _selectedItemCssClass = "selected";
		private DisplayType _typeOfDisplay = DisplayType.FromRoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				int nodeId = GetSelectedNodeId();
				if (nodeId <= 0)
				{
					nodeId = NodeId;
				}
				var selectedNodes = new List<Node>();
				Node node = null;
				switch (TypeOfDisplay)
				{
					case DisplayType.FromRoot:
						node = FindCurrentRootNode(nodeId, out selectedNodes);
						break;
					case DisplayType.FromParent:
						node = FindCurrentParentNode(nodeId, out selectedNodes);
						break;
				}
				Tree tree = GetTree(node, selectedNodes);
				if (tree != null)
				{
				    tree.CssClass = "topnav";
					ltMenu.Text = tree.ToUnordedHtmlList(SelectedItemCssClass);
				}
			}
		}

		private Tree GetTree(Node node, List<Node> selectedNodes)
		{
			if (node == null) return null;
			var bNode = new BNode(_context);
			var nodes = bNode.GetAllChilds(node.Id, true, true);
			var tree = new Tree();
			if ((nodes != null) && (nodes.Count > 0))
			{
				foreach (Node treeNode in nodes)
				{
					var item = new TreeNode(treeNode.Name, GetNodeUrl(treeNode));
					if (selectedNodes.Contains(treeNode))
					{
						item.Selected = true;
					}
					if (treeNode.IsMenu)
					{
						item.Link = null;
					}
					tree.Nodes.Add(item);
					PopulateChilds(item, treeNode.Childs, selectedNodes);
				}
			}
			return tree;
		}

		private void PopulateChilds(TreeNode treeNode, List<Node> nodes, List<Node> selectedNodes)
		{
			if ((nodes != null) && (nodes.Count > 0))
			{
				foreach (Node node in nodes)
				{
					var item = new TreeNode(node.Name, GetNodeUrl(node));
					if (selectedNodes.Contains(node))
					{
						item.Selected = true;
					}
					if (node.IsMenu)
					{
						item.Link = null;
					}
					treeNode.Nodes.Add(item);
					PopulateChilds(item, node.Childs, selectedNodes);
				}
			}
		}

		private string GetNodeUrl(Node node)
		{
			return string.Concat(OpqSubPageUrl, "?nodeId=", node.Id);
		}

		private Node FindCurrentRootNode(int nodeId, out List<Node> selectedNodes)
		{
			Node node = null;
			selectedNodes = new List<Node>();
			if (nodeId <= 0) return null;
			var bNode = new BNode(_context);
			try
			{
				node = bNode.GetNode(nodeId, false); 
			}
			catch (ObjectNotFoundException ex)
			{
				LogException(ex);
			}
			if (node == null) return null;
			selectedNodes.Insert(0, node);
			while (node.Parent.HasValue)
			{
				node = bNode.GetNode((int) node.Parent, false);
				selectedNodes.Insert(0, node);
			}
			return node;
		}

		private Node FindCurrentParentNode(int nodeId, out List<Node> selectedNodes)
		{
			Node node = null;
			selectedNodes = new List<Node>();
			if (nodeId <= 0) return null;
			var bNode = new BNode(_context);
			try
			{
				node = bNode.GetNode(nodeId, false);
			}
			catch (ObjectNotFoundException ex)
			{
				LogException(ex);
			}
			if (node == null) return null;
			selectedNodes.Insert(0, node);
			if (node.Parent.HasValue)
			{
				node = bNode.GetNode((int)node.Parent, false);
				selectedNodes.Insert(0, node);
			}
			return node;
		}

		private int GetSelectedNodeId()
		{
			int nodeId = 0;
			if (Request.QueryString["nodeId"] != null)
			{
				int.TryParse(Request.QueryString["nodeId"], out nodeId);
			}
			return nodeId;
		}

		#region Properties

		/// <summary>
		/// Type of display for tree
		/// </summary>
		public DisplayType TypeOfDisplay
		{
			get { return _typeOfDisplay; }
			set { _typeOfDisplay = value; }
		}

		/// <summary>
		/// Amount of levels to expand
		/// </summary>
		public int ExpandableLevels { get; set; }

		/// <summary>
		/// Getter/Setter for the css class connected to the selected category
		/// </summary>
		public string SelectedItemCssClass
		{
			get { return _selectedItemCssClass; }
			set { _selectedItemCssClass = value; }
		}

		/// <summary>
		/// Getter/Setter for OpqSubPage url 
		/// </summary>
		public string OpqSubPageUrl { get; set; }

		/// <summary>
		/// The nodeid to start build the menu from
		/// </summary>
		public int NodeId { get; set; }
		#endregion
	}
}