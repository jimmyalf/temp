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
	public partial class OpqMenu : UserControl
	{
		private OPQ.Core.Context _context;
		private string _selectedItemCssClass = "selected";

		protected void Page_Load(object sender, EventArgs e)
		{
			_context = SessionContext.CurrentOpq;
			if (!Page.IsPostBack)
			{
				int nodeId = GetSelectedNodeId();
				List<Node> selectedNodes;
				Node node = FindCurrentRootNode(nodeId, out selectedNodes);
				Tree tree = GetTree(node, selectedNodes);
				if (tree != null)
				{
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
			catch (ObjectNotFoundException ex) { }
			if (node == null) return null;
			selectedNodes.Insert(0, node);
			while (node.Parent.HasValue)
			{
				node = bNode.GetNode((int) node.Parent, false);
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


		#endregion
	}
}