using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	/// <summary>
	/// Object for storing a tree structure with methods for rendering a ordered or unordered html list.
	/// </summary>
	public class Tree
	{
		#region Internal parameters & Constructor 

		private string _id = "category-tree";
		private string _cssClass = "tree";

		/// <summary>
		/// Default constructor
		/// </summary>
		public Tree()
		{
			Nodes = new List<TreeNode>();
		}

		#endregion

		#region Member variables

		/// <summary>
		/// Get/Set the treenode list for the tree
		/// </summary>
		public List<TreeNode> Nodes { get; set; }

		/// <summary>
		/// Get/Set the id to be used on the root element when rendering the tree 
		/// </summary>
		public string Id
		{
			get { return _id; }
			set { _id = value;}
		}

		/// <summary>
		/// Get/Set the class to be used on the root element when rendering the tree 
		/// </summary>
		public string CssClass
		{
			get { return _cssClass; }
			set { _cssClass = value; }
		}

		#endregion

		#region Public methods

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

		#region Private methods

		private string ToHtmlList(string selectedCssClass, string tag)
		{
			var innerList = new StringBuilder();
			if ((Nodes != null) && (Nodes.Count > 0))
			{
				innerList.Append(string.Concat("<",tag," id=\"", Id, "\" class=\"", CssClass, "\">"));
				foreach (var node in Nodes)
				{
					innerList.Append(node.ToUnordedHtmlList(selectedCssClass));
				}
				innerList.Append(string.Concat("</",tag,">"));
			}
			return innerList.ToString();
		}

		#endregion

	}
}