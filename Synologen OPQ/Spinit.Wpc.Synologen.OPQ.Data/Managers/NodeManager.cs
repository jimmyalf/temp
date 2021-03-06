﻿using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Data.Managers
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
		/// <exception cref="NodeException">If the parent is not found or the new name exist.</exception>

		private void Insert (ENode node)
		{
			if (node.Parent != null) {
				try {
					GetNodeById ((int) node.Parent);
				}
				catch (ObjectNotFoundException e) {
					if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
						throw new NodeException ("Parent does not exist.", NodeErrors.ParentDoesNotExist);
					}
					throw;
				}
			}

			if (CheckNameExist (node.Name, node.Parent, null)) {
				throw new NodeException ("Name exist.", NodeErrors.NameExist);
			}
			
			node.Order = 0;
			node.CreatedById = Manager.WebContext.UserId ?? 0;
			node.CreatedByName = Manager.WebContext.UserName;
			node.CreatedDate = DateTime.Now;

			if ((node.CreatedById == 0) || (node.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			node.LockedById = Manager.WebContext.UserId ?? 0;
			node.LockedByName = Manager.WebContext.UserName;
			node.LockedDate = DateTime.Now;

			Manager.ExternalObjectsManager.CheckUserExist (node.CreatedById);
			
			node.IsActive = true;

			_insertedNode = node;

			_dataContext.Nodes.InsertOnSubmit (node);
		}

		/// <summary>
		/// Inserts a node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="NodeException">If the parent is not found.</exception>

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

		#region Change

		#region Change Node

		/// <summary>
		/// Updates a node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>
		/// <exception cref="NodeException">
		/// 1. If the parent is not found or the new name exist.
		/// 2. If node is locked by other user.</exception>

		private void Update (ENode node)
		{
			ENode oldNode = _dataContext.Nodes.Single (d => d.Id == node.Id);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;

			if ((oldNode.ChangedById == 0) || (oldNode.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.ChangedById);

			if ((node.Parent != null) && (oldNode.Parent != node.Parent)) {
				if (node.Parent == 0) {
					oldNode.Parent = null;
				}
				else {
					if (node.Parent != null) {
						try {
							GetNodeById ((int) node.Parent);
						}
						catch (ObjectNotFoundException e) {
							if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
								throw new NodeException ("Parent does not exist.", NodeErrors.ParentDoesNotExist);
							}
							throw;
						}
					}
					
					oldNode.Parent = node.Parent;
				}
			}

			if ((node.Name != null) && !oldNode.Name.Equals (node.Name)) {
				if (CheckNameExist (node.Name, node.Parent, node.Id)) {
					throw new NodeException ("Name exist.", NodeErrors.NameExist);
				}
				
				oldNode.Name = node.Name.Length == 0 ? null : node.Name;
			}

			if (node.IsMenu != oldNode.IsMenu) {
				throw new NodeException ("Change of is-menu not allowed!", NodeErrors.ChangeOfIsMenuNotAllowed);
			}
		}

		/// <summary>
		/// Updates a node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>
		/// <exception cref="NodeException">If the parent is not found.</exception>
		/// <exception cref="NodeException">
		/// 1. If the parent is not found or the new name exist.
		/// 2. If node is locked by other user.</exception>

		public void Update (Node node)
		{
			Update (ENode.Convert (node));
		}

		#endregion

		#region Deactive & Reactivate Node

		/// <summary>
		/// Deactivates a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>
		/// <exception cref="NodeException">If node is locked by other user.</exception>

		public void DeactivateNode (int nodeId)
		{
			ENode oldNode = _dataContext.Nodes.Single (n => n.Id == nodeId);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;

			if ((oldNode.ChangedById == 0) || (oldNode.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.ChangedById);

			oldNode.IsActive = false;
		}

		/// <summary>
		/// Reactivates a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>
		/// <exception cref="NodeException">If node is locked by other user.</exception>

		public void ReactivateNode (int nodeId)
		{
			ENode oldNode = _dataContext.Nodes.Single (d => d.Id == nodeId);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;

			if ((oldNode.ChangedById == 0) || (oldNode.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.ChangedById);

			oldNode.IsActive = true;
		}

		#endregion

		#region Move Node

		/// <summary>
		/// Moves a node.
		/// </summary>
		/// <param name="node">The node to move.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>
		/// <exception cref="NodeException">
		/// 1. If the parent is not found, the move is forbidden or the position is not changed.
		/// 2. If node is locked by other user.</exception>

		private void MoveNode (ENode node)
		{
			ENode oldNode = _dataContext.Nodes.Single (n => n.Id == node.Id);

			oldNode.ClearChilds ();

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;

			if ((oldNode.ChangedById == 0) || (oldNode.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.ChangedById);

			if ((oldNode.Order == node.Order) && (oldNode.Parent == node.Parent)) {
				throw new NodeException ("Position not changed.", NodeErrors.PositionNotMoved);
			}

			if (node.Order < 1) 
			{
				node.Order = 1; 
			}
			if (node.Order > (GetNumberOfChilds(node.Parent, false, false) + 1))
			{
				node.Order = GetNumberOfChilds(node.Parent, false, false) + 1;
			}

			//if ((node.Order < 1) || (node.Order > (GetNumberOfChilds (node.Parent, false, false) + 1))) {
			//    throw new NodeException ("Position not valid.", NodeErrors.MoveToForbidden);
			//}

			if (oldNode.Order != node.Order) {
				oldNode.Order = node.Order;
			}
			
			if ((node.Parent != null) && (oldNode.Parent != node.Parent)) {
				if (node.Parent == 0) {
					oldNode.Parent = null;
				}
				else {
					if (node.Parent != null) {
						try {
							GetNodeById ((int) node.Parent);
						}
						catch (ObjectNotFoundException e) {
							if ((ObjectNotFoundErrors) e.ErrorCode
								== ObjectNotFoundErrors.NodeNotFound) {
								throw new NodeException ("Parent does not exist.", NodeErrors.ParentDoesNotExist);
							}
							throw;
						}
					}

					oldNode.Parent = node.Parent;
				}
			}
		}

		/// <summary>
		/// Moves a node.
		/// </summary>
		/// <param name="node">The node to move.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>
		/// <exception cref="NodeException">If the parent is not found, the move is forbidden or the position is not changed.</exception>

		public void MoveNode (Node node)
		{
			MoveNode (ENode.Convert (node));
		}

		#endregion

		#region Approve, Check-Out and Check-In nodes

		/// <summary>
		/// Approves a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node or user is not found.</exception>
		/// <exception cref="NodeException">If node is locked by other user.</exception>

		public void ApproveNode (int nodeId)
		{
			ENode oldNode = _dataContext.Nodes.Single (d => d.Id == nodeId);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.ApprovedById = Manager.WebContext.UserId ?? 0;
			oldNode.ApprovedByName = Manager.WebContext.UserName;
			oldNode.ApprovedDate = DateTime.Now;
			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;
		
			if ((oldNode.ApprovedById == 0) || (oldNode.ApprovedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.ApprovedById);
		}

		/// <summary>
		/// Checks out a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node or user is not found.</exception>
		/// <exception cref="NodeException">If node is locked by other user.</exception>

		public void CheckOutNode (int nodeId)
		{
			ENode oldNode = _dataContext.Nodes.Single (d => d.Id == nodeId);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.LockedById = Manager.WebContext.UserId ?? 0;
			oldNode.LockedByName = Manager.WebContext.UserName;
			oldNode.LockedDate = DateTime.Now;
			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;

			if ((oldNode.LockedById == 0) || (oldNode.LockedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.LockedById);
		}

		/// <summary>
		/// Checks-in a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the node or user is not found.</exception>
		/// <exception cref="NodeException">If node is locked by other user.</exception>

		public void CheckInNode (int nodeId)
		{
			ENode oldNode = _dataContext.Nodes.Single (d => d.Id == nodeId);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			oldNode.LockedById = null;
			oldNode.LockedByName = null;
			oldNode.LockedDate = null;
			oldNode.ChangedById = Manager.WebContext.UserId ?? 0;
			oldNode.ChangedByName = Manager.WebContext.UserName;
			oldNode.ChangedDate = DateTime.Now;

			if ((oldNode.ChangedById == 0) || (oldNode.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldNode.ChangedById);
		}

		#endregion

		#endregion

		#region Remove

		#region Remove Node

		/// <summary>
		/// Deletes a specific node including node-suppliers.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		private void Delete (ENode node)
		{
			ENode oldNode = _dataContext.Nodes.Single (n => n.Id == node.Id);

			if (oldNode == null) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			if ((oldNode.LockedById != null) && (oldNode.LockedById != Manager.WebContext.UserId)) {
				throw new NodeException ("Node locked by another user.", NodeErrors.NodeLockedByOtherUser);
			}

			try {
				Delete (oldNode.Id);
			}
			catch (ObjectNotFoundException e) {
				if ((ObjectNotFoundErrors) e.ErrorCode != ObjectNotFoundErrors.NodeSupplierNotFound) {
					throw;
				}
			}

			_dataContext.Nodes.DeleteOnSubmit (oldNode);
		}

		/// <summary>
		/// Deletes a specific node including node-supplier-connections.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public void Delete (Node node)
		{
			Delete (ENode.Convert (node));
		}

		#endregion

		#region Remove Node Supplier

		/// <summary>
		/// Deletes a specific node-supplier-connection.
		/// </summary>
		/// <param name="nodeSupplierConnection">The node-supplier-connection.</param>
		/// <exception cref="ObjectNotFoundException">If the node-supplier-connection is not found.</exception>

		private void Delete (ENodeSupplierConnection nodeSupplierConnection)
		{
			ENodeSupplierConnection oldNodeSupplierConnection
				= _dataContext.NodeSupplierConnections.Single (
					nsc => nsc.NdeId == nodeSupplierConnection.NdeId && nsc.SupId == nodeSupplierConnection.SupId);

			if (oldNodeSupplierConnection == null) {
				throw new ObjectNotFoundException (
					"Node-supplier-connection not found.",
					ObjectNotFoundErrors.NodeSupplierNotFound);
			}

			_dataContext.NodeSupplierConnections.DeleteOnSubmit (oldNodeSupplierConnection);
		}

		/// <summary>
		/// Deletes a specific node-supplier-connection.
		/// </summary>
		/// <param name="nodeSupplierConnection">The node-supplier-connection.</param>
		/// <exception cref="ObjectNotFoundException">If the node-supplier-connection is not found.</exception>

		public void Delete (NodeSupplierConnection nodeSupplierConnection)
		{
			Delete (ENodeSupplierConnection.Convert (nodeSupplierConnection));
		}

		/// <summary>
		/// Deletes all node-supplier-connections for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="ObjectNotFoundException">If the node-supplier-connection is not found.</exception>

		public void Delete (int nodeId)
		{
			var query = from nodeSupplierConnection in _dataContext.NodeSupplierConnections
						where nodeSupplierConnection.NdeId == nodeId
						select nodeSupplierConnection;


			IList<ENodeSupplierConnection> documentHistories = query.ToList ();

			if (documentHistories.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node-supplier-connection not found.",
					ObjectNotFoundErrors.NodeSupplierNotFound);
			}

			_dataContext.NodeSupplierConnections.DeleteAllOnSubmit (documentHistories);
		}

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
			IQueryable<ENode> query = from node in _dataContext.Nodes
			            where node.Id == nodeId
			            select node;

			IList<ENode> nodes = query.ToList ();

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return Converter (nodes.First ());
		}

		/// <summary>
		/// Fetches the node by parent and name.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="name">The name of the node.</param>
		/// <returns>A node.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public Node GetNodeByName (int? parent, string name)
		{
			IQueryable<ENode> query = from node in _dataContext.Nodes
									  where node.Name == name
									  select node;

			query = parent == null ? query.AddIsNullCondition ("Parent") : query.AddEqualityCondition ("Parent", parent);

			IList<ENode> nodes = query.ToList ();

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return Converter (nodes.First ());
		}

		/// <summary>
		/// Fetches all root-nodes.
		/// </summary>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of nodes.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public IList<Node> GetRootNodes (bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<ENode> query = from node in _dataContext.Nodes
			            where node.Parent == null
						orderby node.Order ascending
						select node;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<ENode, Node> converter = Converter;
			IList<Node> nodes = query.ToList ().ConvertAll (converter);

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return nodes;
		}

		/// <summary>
		/// Fetches all nodes for a specific parent.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of nodes.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public IList<Node> GetChildNodes (int parent, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<ENode> query = from node in _dataContext.Nodes
						where node.Parent == parent
						orderby node.Order ascending 
						select node;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<ENode, Node> converter = Converter;
			IList<Node> nodes = query.ToList ().ConvertAll (converter);

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return nodes;
		}

		/// <summary>
		/// Fetches all nodes for a specific parent.
		/// </summary>
		/// <param name="name">The name to search for.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of nodes.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public IList<Node> GetNodesByName (string name, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<ENode> query = from node in _dataContext.Nodes
						where SqlMethods.Like (node.Name, string.Concat ("%", name, "%"))
						orderby node.Order ascending
						select node;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<ENode, Node> converter = Converter;
			IList<Node> nodes = query.ToList ().ConvertAll (converter);

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return nodes;
		}

		/// <summary>
		/// Fetches all nodes for a specific parent.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="name">The name to search for.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of nodes.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public IList<Node> GetNodesByName (int parent, string name, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<ENode> query = from node in _dataContext.Nodes
						where node.Parent == parent
							&& SqlMethods.Like (node.Name, string.Concat ("%", name, "%"))
						orderby node.Order ascending
						select node;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<ENode, Node> converter = Converter;
			IList<Node> nodes = query.ToList ().ConvertAll (converter);

			if (nodes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node not found.",
					ObjectNotFoundErrors.NodeNotFound);
			}

			return nodes;
		}

		/// <summary>
		/// Fetches a list of nodes from node to root.
		/// </summary>
		/// <returns>A list of nodes.</returns>
		/// <param name="nodeId">The node-id to search from.</param>
		/// <param name="rootFirst">If true=>root is first in list.</param>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public IList<Node> GetListUp (int nodeId, bool rootFirst)
		{
			IList<Node> nodes = new List<Node> ();
			Node node = new Node {Parent = nodeId};
			do {
				int parent = (int) node.Parent;
				var query = from qNode in _dataContext.Nodes
							where qNode.Id == parent
							orderby qNode.Order ascending
							select qNode;
				
				IList<ENode> tmpNodes = query.ToList ();

				if (tmpNodes.IsEmpty ()) {
					throw new ObjectNotFoundException (
						"Node not found.",
						ObjectNotFoundErrors.NodeNotFound);
				}

				node = Converter (tmpNodes.First ());

				if (rootFirst) {
					nodes.Insert (0, node);
				}
				else {
					nodes.Add (node);
				}
			} while (node.Parent != null);

			return nodes;
		}

		/// <summary>
		/// Counts the number of childs for a parent. 
		/// </summary>
		/// <param name="parent">The parent (for root null).</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>The number of childs.</returns>

		public int GetNumberOfChilds (int? parent, bool onlyActive, bool onlyApproved)
		{
			if (onlyApproved) {
				if (onlyActive) {
					if (parent == null) {
						return _dataContext.Nodes.Count (
							node => node.Parent == null 
									&& node.IsActive
									&& node.ApprovedById != null
									&& node.LockedById == null);
					}

					return _dataContext.Nodes.Count (
						node => node.Parent == parent
								&& node.IsActive
								&& node.ApprovedById != null
								&& node.LockedById == null);
				}

				if (parent == null) {
					return _dataContext.Nodes.Count (
						node => node.Parent == null
								&& node.ApprovedById != null
								&& node.LockedById == null);
				}

				return _dataContext.Nodes.Count (
						node => node.Parent == parent
								&& node.ApprovedById != null
								&& node.LockedById == null);
			}
			
			if (onlyActive) {
				if (parent == null) {
					return _dataContext.Nodes.Count (node => node.Parent == null && node.IsActive);
				}

				return _dataContext.Nodes.Count (node => node.Parent == parent && node.IsActive);
			}

			if (parent == null) {
				return _dataContext.Nodes.Count (node => node.Parent == null);
			}

			return _dataContext.Nodes.Count (node => node.Parent == parent);
		}

		/// <summary>
		/// Fetches all nodes including childs from a specified parent.
		/// </summary>
		/// <param name="parent">The parent (if null fetch roots).</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of nodes.</returns>
		/// <exception cref="ObjectNotFoundException">If the node is not found.</exception>

		public IList<Node> GetAllChildsDown (int? parent, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<ENode> query = from node in _dataContext.Nodes
			                                orderby node.Order ascending
			                                select node;

			query = parent == null ? query.AddIsNullCondition ("Parent") : query.AddEqualityCondition ("Parent", parent);

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<ENode, Node> converter = Converter;
			IList<Node> nodes = query.ToList ().ConvertAll (converter);

			if (!nodes.IsEmpty ()) {
				foreach (Node node in nodes) {
					node.Childs = (List<Node>) GetAllChildsDown (node.Id, onlyActive, onlyApproved);
				}
			}

			return nodes;
		}

		#endregion

		#region Fetch Node Supplier

		/// <summary>
		/// Fetches a specified node-supplier-connection.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="supplierId">The supplier-id.</param>
		/// <returns>A node-supplier-connection.</returns>
		/// <exception cref="ObjectNotFoundException">If the node-supplier-connection is not found.</exception>

		public NodeSupplierConnection GetNodeSupplierConnectionById (int nodeId, int supplierId)
		{
			IQueryable<ENodeSupplierConnection> query = from nodeSupplierConnection in _dataContext.NodeSupplierConnections
			                                            where nodeSupplierConnection.NdeId == nodeId
															&& nodeSupplierConnection.SupId == supplierId
														select nodeSupplierConnection;

			IList<ENodeSupplierConnection> nodeSupplierConnections = query.ToList ();

			if (nodeSupplierConnections.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node-supplier-connection not found.",
					ObjectNotFoundErrors.NodeSupplierNotFound);
			}

			return Converter (nodeSupplierConnections.First ());
		}

		/// <summary>
		/// Fetches all node-supplier-connections for a specific node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <returns>A list of node-supplier-connections.</returns>
		/// <exception cref="ObjectNotFoundException">If the node-supplier-connection is not found.</exception>

		public IList<NodeSupplierConnection> GetNodeSupplierConnectionsForNode (int nodeId)
		{
			IOrderedQueryable<ENodeSupplierConnection> query =
				from nodeSupplierConnection in _dataContext.NodeSupplierConnections
				where nodeSupplierConnection.NdeId == nodeId
				orderby nodeSupplierConnection.SupId ascending
				select nodeSupplierConnection;

			Converter<ENodeSupplierConnection, NodeSupplierConnection> converter = Converter;
			IList<NodeSupplierConnection> nodeSupplierConnections = query.ToList ().ConvertAll (converter);

			if (nodeSupplierConnections.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node-supplier-connection not found.",
					ObjectNotFoundErrors.NodeSupplierNotFound);
			}

			return nodeSupplierConnections;
		}

		/// <summary>
		/// Fetches all node-supplier-connections for a specific supplier.
		/// </summary>
		/// <param name="supplierId">The supplier-id.</param>
		/// <returns>A list of node-supplier-connections.</returns>
		/// <exception cref="ObjectNotFoundException">If the node-supplier-connection is not found.</exception>

		public IList<NodeSupplierConnection> GetNodeSupplierConnectionsForSupplier (int supplierId)
		{
			IOrderedQueryable<ENodeSupplierConnection> query =
				from nodeSupplierConnection in _dataContext.NodeSupplierConnections
				where nodeSupplierConnection.SupId == supplierId
				orderby nodeSupplierConnection.NdeId ascending
				select nodeSupplierConnection;

			Converter<ENodeSupplierConnection, NodeSupplierConnection> converter = Converter;
			IList<NodeSupplierConnection> nodeSupplierConnections = query.ToList ().ConvertAll (converter);

			if (nodeSupplierConnections.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Node-supplier-connection not found.",
					ObjectNotFoundErrors.NodeSupplierNotFound);
			}

			return nodeSupplierConnections;
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

			if (!SkipNodes && (eNode.Childs != null))
			{
				SkipNodes = true;
				Converter<ENode, Node> converter = Converter;
				node.Childs = eNode.Childs.ToList().ConvertAll(converter);
				SkipNodes = false;
			}

			if (!SkipNode && eNode.ParentNode != null)
			{
				SkipParent = true;
				node.ParentNode = Converter(eNode.ParentNode);
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

		#region Internal methods

		/// <summary>
		/// Checks to see if the name exist under the choosen parent.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="parent">The parent-id.</param>
		/// <param name="id">The id of the node itself.</param>
		/// <returns>If name exists=>true otherwise false.</returns>

		private bool CheckNameExist (string name, int? parent, int? id)
		{
			IQueryable<ENode> query = from node in _dataContext.Nodes
			                          where node.Name == name
			                          select node;

			query = parent == null ? query.AddIsNullCondition ("Parent") : query.AddEqualityCondition ("Parent", parent);

			if (id != null) {
				query.AddNotEqualityCondition ("Id", (int) id);
			}

			IList<ENode> tmpNodes = query.ToList ();

			if (!tmpNodes.IsEmpty ()) {
				return true;
			}

			return false;
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
