using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The nodeFill business class.
	/// Implements the classes tblSynologenOPQNodes and tblSynologenOPQNodeLocationLanguageConnection.
	/// </summary>
	public class BNode
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>
		public BNode (Context context)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public Context Context
		{
			get
			{
				throw new System.NotImplementedException ();
			}
			set
			{
			}
		}
		/// <summary>
		/// Creates a new node.
		/// </summary>
		/// <param name="parent">The parent identity.</param>
		/// <param name="name">The name of the node.</param>
		/// <param name="node">The created node.</param>
		/// <param name="nodeFill">The created nodeFill.</param>
		public SynologenOpqNode CreateNode (int? parent, string name)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Changes a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="parent">The node's parent nodeId.</param>
		/// <param name="name">The node's name.</param>
		/// <param name="node">The changed node.</param>
		/// <param name="nodeFill">The changed nodeFill.</param>
		public SynologenOpqNode ChangeNode (int nodeId, int? parent, string name)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Deletes a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		public void DeleteNode (int nodeId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Publishes a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		public void Publish (int nodeId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Moves the node in the tree.
		/// </summary>
		/// <param name="type">The type of move.</param>
		/// <param name="source">The node to be moved.</param>
		/// <param name="destination">The destination reference node.</param>
		public void MoveNode (NodeMoveActions type, int source, int? destination)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Fetches a specified node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="node">The node.</param>
		public SynologenOpqNode GetNode (int nodeId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a list of nodes.
		/// </summary>
		/// <param name="parent">The parent to fetch nodes from.</param>
		/// <param name="name">The node-name to search for.</param>
		/// <param name="onlyActive">If true=&gt;only fetch active nodes.</param>
		/// <param name="nodeFill">The node-fill object.</param>
		/// <param name="nodes">The fetched nodes.</param>
		public List<SynologenOpqNode> GetNodes (int? parent, string name, bool onlyActive)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Locks a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		public void Lock (string nodeId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Unlocks the node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		public void UnLock (int nodeId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Adds a supplier.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="supplierId">The id of the supplier.</param>
		public void AddSupplier (int nodeId, int supplierId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Removes a supplier.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="supplierId">The id of the supplier.</param>
		public void RemoveSupplier (int nodeId, int supplierId)
		{
			throw new System.NotImplementedException ();
		}
	}
}
