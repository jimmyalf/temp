using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The nodeFill business class.
	/// Implements the tables SynologenOPQNodes.
	/// </summary>
	
	public class BNode
	{
		private readonly Context _context;
		private readonly Configuration _configuration;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>
		
		public BNode (Context context)
		{
			_context = context;
			_configuration = Configuration.GetConfiguration (_context);
		}

		#region Node

		/// <summary>
		/// Creates a new node.
		/// </summary>
		/// <param name="parent">The parent identity.</param>
		/// <param name="name">The name of the node.</param>
		/// <param name="isMenu">If true=>is menu.</param>

		public Node CreateNode (int? parent, string name, bool isMenu)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.Insert (new Node {Parent = parent, Name = name, IsMenu = isMenu});
				synologenRepository.SubmitChanges ();

				return synologenRepository.Node.GetInsertedNode ();
			}
		}

		/// <summary>
		/// Changes a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="parent">The node's parent nodeId.</param>
		/// <param name="name">The node's name.</param>

		public Node ChangeNode (int nodeId, int? parent, string name)
		{
			Node node = GetNode (nodeId, false);
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				node.Parent = parent;
				node.Name = name;
					
				synologenRepository.Node.Update (node);
				synologenRepository.SubmitChanges ();

				return synologenRepository.Node.GetNodeById (nodeId);
			}
		}

		/// <summary>
		/// Deletes a node:
		/// if removeCompletely = false: sets the is-active to false.
		/// if removeCompletely = true: deletes the node and all connecting objects.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="removeCompletely">If true=>removes a node completely.</param>

		public void DeleteNode (int nodeId, bool removeCompletely)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				
				if (removeCompletely) {
					// Remove all documents
					try {
						synologenRepository.Document.DeleteAllForNode (nodeId);
					}
					catch (ObjectNotFoundException e) {
						if ((ObjectNotFoundErrors) e.ErrorCode != ObjectNotFoundErrors.DocumentNotFound) {
							throw;
						}
					}

					// Remove all files
					try {
						synologenRepository.File.DeleteAllForNode (nodeId);
					}
					catch (ObjectNotFoundException e) {
						if ((ObjectNotFoundErrors) e.ErrorCode != ObjectNotFoundErrors.FileNotFound) {
							throw;
						}
					}

					// Remove node
					synologenRepository.Node.Delete (new Node {Id = nodeId});

				}
				else {
					synologenRepository.Node.DeactivateNode (nodeId);
				}
					
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Undeletes a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>

		public void UnDeleteNode (int nodeId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.ReactivateNode (nodeId);			
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Publishes a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		
		public void Publish (int nodeId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.ApproveNode (nodeId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Locks a node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		
		public void Lock (int nodeId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.CheckOutNode (nodeId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Unlocks the node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		
		public void UnLock (int nodeId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.CheckInNode (nodeId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Moves the node in the tree.
		/// </summary>
		/// <param name="moveAction">The move-action.</param>
		/// <param name="source">The node to be moved.</param>
		/// <param name="destination">The destination reference node.</param>
		
		public void MoveNode (NodeMoveActions moveAction, int source, int? destination)
		{
			Node sSource = GetNode (source, false);
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				Node sDestination = null;
				if (destination != null) {
					sDestination = synologenRepository.Node.GetNodeById ((int) destination);
				}
				switch (moveAction) {
					case NodeMoveActions.MoveUp:
						synologenRepository.Node.MoveNode (new Node {Id = source, Parent = sSource.Parent, Order = sSource.Order - 1});
						break;

					case NodeMoveActions.MoveDown:
						synologenRepository.Node.MoveNode (new Node { Id = source, Parent = sSource.Parent, Order = sSource.Order + 1 });
						break;

					case NodeMoveActions.MoveAfter:
						if (sDestination == null) {
							throw new NodeException ("Not valid move operation.", NodeErrors.MoveToForbidden);
						}
						
						synologenRepository.Node.MoveNode (
							new Node { Id = source, Parent = sDestination.Parent, Order = sDestination.Order + 1 });
						break;

					case NodeMoveActions.MoveBefore:
						if (sDestination == null)
						{
							throw new NodeException("Not valid move operation.", NodeErrors.MoveToForbidden);
						}

						synologenRepository.Node.MoveNode(
							new Node { Id = source, Parent = sDestination.Parent, Order = sDestination.Order - 1 });
						break;

					case NodeMoveActions.MoveInto:
						if (sDestination == null) {
							throw new NodeException ("Not valid move operation.", NodeErrors.MoveToForbidden);
						}

						synologenRepository.Node.MoveNode (
							new Node
							{
								Id = source, 
								Parent = sDestination.Id, 
								Order = synologenRepository.Node.GetNumberOfChilds (sDestination.Id, false, false) + 1
							});
						break;

					default:
						throw new NodeException ("Not valid move operation.", NodeErrors.MoveToForbidden);
				}
				
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Fetches a specified node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="fillObjects">Fill all objects.</param>

		public Node GetNode (int nodeId, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Node> (n => n.NodeSupplierConnections);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Files);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Documents);
					synologenRepository.AddDataLoadOptions<Node> (n => n.CreatedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ChangedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.LockedBy);

					synologenRepository.AddDataLoadOptions<File> (f => f.BaseFile);
					synologenRepository.AddDataLoadOptions<File> (f => f.CreatedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ChangedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ApprovedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.LockedBy);

					synologenRepository.AddDataLoadOptions<Document> (d => d.CreatedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ChangedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.LockedBy);

					synologenRepository.SetDataLoadOptions ();
				}

				Node node =  synologenRepository.Node.GetNodeById (nodeId);

				node.ParentNode = fillObjects && (node.Parent != null)
				                  	? synologenRepository.Node.GetNodeById ((int) node.Parent)
				                  	: null;

				return node;
			}
		}

		/// <summary>
		/// Fetches a specified node.
		/// </summary>
		/// <param name="parent">The parent-id of the node.</param>
		/// <param name="name">The name of the node.</param>
		/// <param name="fillObjects">Fill all objects.</param>

		public Node GetNode (int? parent, string name, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Node> (n => n.NodeSupplierConnections);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Files);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Documents);
					synologenRepository.AddDataLoadOptions<Node> (n => n.CreatedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ChangedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.LockedBy);

					synologenRepository.AddDataLoadOptions<File> (f => f.BaseFile);
					synologenRepository.AddDataLoadOptions<File> (f => f.CreatedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ChangedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ApprovedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.LockedBy);

					synologenRepository.AddDataLoadOptions<Document> (d => d.CreatedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ChangedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.LockedBy);

					synologenRepository.SetDataLoadOptions ();
				}

				Node node = synologenRepository.Node.GetNodeByName (parent, name);

				node.ParentNode = fillObjects && (node.Parent != null)
									? synologenRepository.Node.GetNodeById ((int) node.Parent)
									: null;

				return node;
			}
		}

		/// <summary>
		/// Gets a list of nodes.
		/// </summary>
		/// <param name="parent">The parent to fetch nodes from.</param>
		/// <param name="name">The node-name to search for.</param>
		/// <param name="onlyActive">If true=&gt;only fetch active nodes.</param>
		/// <param name="onlyApproved">If true=>fetch only approved documents.</param>
		/// <param name="fillObjects">Fill all objects.</param>

		public IList<Node> GetNodes (int? parent, string name, bool onlyActive, bool onlyApproved, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Node> (n => n.NodeSupplierConnections);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Files);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Documents);
					synologenRepository.AddDataLoadOptions<Node> (n => n.CreatedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ChangedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.LockedBy);

					synologenRepository.AddDataLoadOptions<File> (f => f.BaseFile);
					synologenRepository.AddDataLoadOptions<File> (f => f.CreatedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ChangedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ApprovedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.LockedBy);

					synologenRepository.AddDataLoadOptions<Document> (d => d.CreatedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ChangedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.LockedBy);
					
					synologenRepository.SetDataLoadOptions ();
				}

				IList<Node> nodes;

				if ((parent == null) && (name == null)) {
					nodes = synologenRepository.Node.GetRootNodes (onlyActive, onlyApproved);
				}
				else if (name == null) {
					nodes = synologenRepository.Node.GetChildNodes ((int) parent, onlyActive, onlyApproved);
				}
				else if (parent == null) {
					nodes = synologenRepository.Node.GetNodesByName (name, onlyActive, onlyApproved);
				}
				else {
					nodes = synologenRepository.Node.GetNodesByName ((int) parent, name, onlyActive, onlyApproved);
				}

				if (fillObjects && (nodes != null) && (nodes.Count > 0)) {
					foreach (Node node in nodes) {
						node.ParentNode = node.Parent != null ? synologenRepository.Node.GetNodeById ((int) node.Parent) : null;
					}
				}

				return nodes;
			}
		}

		/// <summary>
		/// Gets a list of nodes with all childs down.
		/// </summary>
		/// <param name="parent">The parent to fetch nodes from.</param>
		/// <param name="onlyActive">If true=&gt;only fetch active nodes.</param>
		/// <param name="onlyApproved">If true=>fetch only approved documents.</param>

		public IList<Node> GetAllChilds (int? parent, bool onlyActive, bool onlyApproved)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				return synologenRepository.Node.GetAllChildsDown (parent, onlyActive, onlyApproved);
			}
		}

		#endregion

		#region Node Supplier

		/// <summary>
		/// Adds a supplier.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="supplierId">The id of the supplier.</param>
		
		public void AddSupplier (int nodeId, int supplierId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.Insert (new NodeSupplierConnection {NdeId = nodeId, SupId = supplierId});
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Removes a supplier.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="supplierId">The id of the supplier.</param>
		
		public void RemoveSupplier (int nodeId, int supplierId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.Delete (new NodeSupplierConnection { NdeId = nodeId, SupId = supplierId });
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Fetches a list of node-suppliers.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="supplierId">The supplier-id.</param>
		/// <returns>A list of node-supplier-connections.</returns>

		public IList<NodeSupplierConnection> GetNodeSuppliers (int? nodeId, int? supplierId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (nodeId != null) {
					return synologenRepository.Node.GetNodeSupplierConnectionsForNode ((int) nodeId);
				}

				if (supplierId != null) {
					return synologenRepository.Node.GetNodeSupplierConnectionsForSupplier ((int) supplierId);
				}

				return null;
			}
		}

		/// <summary>
		/// Fetches a node-supplierr.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="supplierId">The supplier-id.</param>
		/// <returns>A list of node-supplier-connections.</returns>

		public NodeSupplierConnection GetNodeSupplier (int nodeId, int supplierId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				
				return synologenRepository.Node.GetNodeSupplierConnectionById (nodeId, supplierId);
			}
		}

		#endregion
	}
}
