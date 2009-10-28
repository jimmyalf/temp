using System;
using System.Collections.Generic;
using System.Linq;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;
using Spinit.Wpc.Synologen.OPQ.Data.Entities;

namespace Spinit.Wpc.Synologen.Opq.Data.Managers
{
	public class NodeManager : EntityManager<WpcSynologenRepository>
	{
		private readonly WpcSynologenDataContext _dataContext;

		private ENode _insertedNode;
		private ENodeSupplierConnection _insertedNodeSupplier;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="manager">The repository-manager.</param>

		public NodeManager (WpcSynologenRepository manager) : base (manager)
		{
			_dataContext = (WpcSynologenDataContext) Manager.Context;
		}

		#region Create

		#region Create Node

		/// <summary>
		/// Inserts a node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="UserException">If no current-user.</exception>

		private void Insert (ENode node)
		{
			node.CreatedById = Manager.WebContext.UserId ?? 0;
			node.CreatedByName = Manager.WebContext.UserName;
			node.CreatedDate = DateTime.Now;

			if ((node.CreatedById == 0) || (node.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			_insertedNode = node;

			_dataContext.Nodes.InsertOnSubmit (node);
		}

		/// <summary>
		/// Inserts a node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="UserException">If no current-user.</exception>

		public void Insert (Node node)
		{
			Insert (ENode.Convert (node));
		}

		/// <summary>
		/// Returns the inserted node.
		/// </summary>
		/// <returns>The inserted node.</returns>

		public Node GetInsertedNode ()
		{
			return ENode.Convert (_insertedNode);
		}

		#endregion

		#region Create Node-Supplier

		/// <summary>
		/// Inserts a node.
		/// </summary>
		/// <param name="nodeSupplierConnection">The node-supplier-connection.</param>
		/// <exception cref="ObjectNotFoundException">If the node or supplier is not found.</exception>

		private void Insert (ENodeSupplierConnection nodeSupplierConnection)
		{
			GetNodeById (nodeSupplierConnection.NdeId);
			Manager.ExternalObjectsManager.GetUserById (nodeSupplierConnection.SupId);

			_insertedNodeSupplier = nodeSupplierConnection;

			_dataContext.NodeSupplierConnections.InsertOnSubmit (nodeSupplierConnection);
		}

		/// <summary>
		/// Inserts a node-supplier-connection.
		/// </summary>
		/// <param name="nodeSupplierConnection">The node-supplier-connection.</param>

		public void Insert (NodeSupplierConnection nodeSupplierConnection)
		{
			Insert (ENodeSupplierConnection.Convert (nodeSupplierConnection));
		}

		/// <summary>
		/// Returns the inserted node-supplier-connection..
		/// </summary>
		/// <returns>The inserted node-supplier-connection.</returns>

		public NodeSupplierConnection GetInsertedNodeSupplierConnection ()
		{
			return ENodeSupplierConnection.Convert (_insertedNodeSupplier);
		}


		#endregion

		#endregion

		#region Update

		#region Update Node

		#endregion

		#endregion

		#region Fetch

		#region Fetch Node

		/// <summary>
		/// Fetches the node by node-id.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <returns>A node.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public Node GetNodeById (int nodeId)
		{
			var query = from node in _dataContext.Nodes
			            where node.Id == nodeId
			            select node;

			IList<ENode> nodes = query.ToList ();

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return ENode.Convert (nodes.First ());
		}

		#endregion

		#endregion

		#region Converters

		/// <summary>
		/// Converts a ENode to a Node with associations.
		/// </summary>
		/// <param name="eNode">The eNode.</param>
		/// <returns>A node.</returns>

		public Node Converter (ENode eNode)
		{
			Node node = ENode.Convert (eNode);

			if (!SkipNodeSuppliers && (eNode.NodeSupplierConnections != null)) {
				SkipNode = true;
				Converter<ENodeSupplierConnection, NodeSupplierConnection> converter = Converter;
				node.NodeSupplierConnections = eNode.NodeSupplierConnections.ToList ().ConvertAll (converter);
				SkipNode = false;
			}

			if (!SkipDocuments && (eNode.Documents != null)) {
				Manager.Document.SkipNode = true;
				Converter<EDocument, Document> converter = Manager.Document.Converter;
				node.Documents = eNode.Documents.ToList ().ConvertAll (converter);
				Manager.Document.SkipNode = false;
			}

			if (!SkipFiles && (eNode.Files != null)) {
				Manager.File.SkipNode = true;
				Converter<EFile, File> converter = Manager.File.Converter;
				node.Files = eNode.Files.ToList ().ConvertAll (converter);
				Manager.File.SkipNode = false;
			}

			if (!SkipNodes && (eNode.Childs != null)) {
				SkipNodes = true;
				Converter<ENode, Node> converter = Converter;
				node.Childs = eNode.Childs.ToList ().ConvertAll (converter);
				SkipNodes = false;
			}

			if (!SkipNode && eNode.ParentNode != null) {
				SkipParent = true;
				node.ParentNode = Converter (eNode.ParentNode);
				SkipParent = false;
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eNode.CreatedBy != null)) {
				node.CreatedBy = EBaseUser.Convert (eNode.CreatedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eNode.ChangedBy != null)) {
				node.ChangedBy = EBaseUser.Convert (eNode.ChangedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eNode.ApprovedBy != null)) {
				node.ApprovedBy = EBaseUser.Convert (eNode.ApprovedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eNode.LockedBy != null)) {
				node.LockedBy = EBaseUser.Convert (eNode.LockedBy);
			}
			
			return node;
		}

		/// <summary>
		/// Converts a ENodeSupplierConnection to a NodeSupplierConnection with associations.
		/// </summary>
		/// <param name="eNodeSupplierConnection">The eNodeSupplierConnection.</param>
		/// <returns>A node-supplier-connection.</returns>

		public NodeSupplierConnection Converter (ENodeSupplierConnection eNodeSupplierConnection)
		{
			NodeSupplierConnection nodeSupplierConnection = ENodeSupplierConnection.Convert (eNodeSupplierConnection);

			if (!SkipNode && (eNodeSupplierConnection.Node != null)) {
				SkipNodeSuppliers = true;
				nodeSupplierConnection.Node = Converter (eNodeSupplierConnection.Node);
				SkipNodeSuppliers = false;
			}

			// Only fetch users flat (external object).
			if (!SkipSupplier && (eNodeSupplierConnection.BaseUser != null)) {
				nodeSupplierConnection.BaseUser = EBaseUser.Convert (eNodeSupplierConnection.BaseUser);
			}

			return nodeSupplierConnection;
		}

		#endregion

		#region Properties

		#region Skips for Node

		/// <summary>
		/// If true=>skip filling node-suppliers.
		/// </summary>

		public bool SkipNodeSuppliers { get; set; }

		/// <summary>
		/// If true=>skip filling documents.
		/// </summary>

		public bool SkipDocuments { get; set; }

		/// <summary>
		/// If true=>skip filling files.
		/// </summary>

		public bool SkipFiles { get; set; }

		/// <summary>
		/// If true=>skip filling nodes.
		/// </summary>

		public bool SkipNodes { get; set; }

		/// <summary>
		/// If true=>skip filling parent.
		/// </summary>

		public bool SkipParent { get; set; }

		/// <summary>
		/// If true=>skip filling users.
		/// </summary>

		public bool SkipUsers { get; set; }

		#endregion

		#region Sips for Node-Supplier

		/// <summary>
		/// If true=>skip filling nodes.
		/// </summary>

		public bool SkipNode { get; set; }

		/// <summary>
		/// If true=>skip filling suppliers.
		/// </summary>

		public bool SkipSupplier { get; set; }

		#endregion

		#endregion
	}
}
