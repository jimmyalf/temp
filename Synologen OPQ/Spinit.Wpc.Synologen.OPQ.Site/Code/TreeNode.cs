using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	/// <summary>
	/// Object for storing a tree node with methods for rendering a ordered or unordered html list.
	/// </summary>

	public class TreeNode
	{
		#region Internal parameters & Constructors
		
		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="name">The name of the leaf</param>
		/// <param name="link">The href for the link</param>
		/// <param name="nodes">The childs for the leaf</param>
		public TreeNode(string name, string link, List<TreeNode> nodes)
		{
			Nodes = new List<TreeNode>();
			Name = name;
			Link = link;
			Nodes = nodes;
		}

		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="name">The name of the leaf</param>
		/// <param name="link">The href for the link</param>
		public TreeNode(string name, string link)
		{
			Nodes = new List<TreeNode>();
			Name = name;
			Link = link;
		}

		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="name">The name of the leaf</param>
		public TreeNode(string name)
		{
			Nodes = new List<TreeNode>();
			Name = name;
		}

		#endregion

		#region Member variables

		/// <summary>
		/// Get/Set for the node name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Get/Set for the node children
		/// </summary>
		public List<TreeNode> Nodes { get; set; }

		/// <summary>
		/// Get/Set for the parent node
		/// </summary>
		public TreeNode Parent { get; set; }

		/// <summary>
		/// Get/Set for the href for the node
		/// </summary>
		public string Link { get; set; }

		/// <summary>
		/// Get/Set if the node should be selected when rendered
		/// </summary>
		public bool Selected { get; set; }

		/// <summary>
		/// Get/Set for the class to be used on the li element
		/// </summary>
		public string CssClass { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// generates a ul list  
		/// </summary>
		/// <param name="selectedCssClass">The class to be used for selected elements in the list</param>
		/// <returns>a string containing the ul list</returns>
		public string ToUnordedHtmlList(string selectedCssClass)
		{
			return ToHtmlList(selectedCssClass, "ul");
		}

		/// <summary>
		/// generates a ul list  
		/// </summary>
		/// <param name="selectedCssClass">The class to be used for selected elements in the list</param>
		/// <returns>a string containing the ol list</returns>
		public string ToOrdedHtmlList(string selectedCssClass)
		{
			return ToHtmlList(selectedCssClass, "ol");
		}

		#endregion

		#region Private Methods

		private string ToHtmlList(string selectedCssClass, string tag)
		{
			string li = "<li{0}>{1}{2}</li>";
			var cssClass = string.Empty;
			if (!string.IsNullOrEmpty(CssClass))
				cssClass = Selected
				           	? string.Concat(" class=\"", CssClass, " ", selectedCssClass, "\"")
				           	: string.Concat(" class=\"", CssClass, "\"");
			else if (Selected) cssClass = string.Concat(" class=\"", selectedCssClass, "\"");
			var liText = string.Empty;
			if (!string.IsNullOrEmpty(Link))
			{
				liText = string.Concat("<a href=\"", Link, "\">", Name, "</a>");
			}
			else
			{
				liText = Name;
			}
			var innerList = new StringBuilder();
			if ((Nodes != null) && (Nodes.Count > 0))
			{
				innerList.Append(string.Concat("<",tag,">"));
				foreach (var node in Nodes)
				{
					innerList.Append(node.ToUnordedHtmlList(selectedCssClass));
				}
				innerList.Append(string.Concat("</",tag,">"));
			}
			return string.Format(li, cssClass, liText, innerList);
		}

		#endregion

	}
}