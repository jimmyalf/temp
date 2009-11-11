using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
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

		public Node CreateNode (int? parent, string name)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.Insert (new Node {Parent = parent, Name = name});
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
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.Update (new Node { Id = nodeId, Parent = parent, Name = name });
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
		/// Moves the node in the tree.
		/// </summary>
		/// <param name="type">The type of move.</param>
		/// <param name="source">The node to be moved.</param>
		/// <param name="destination">The destination reference node.</param>
		
		public void MoveNode (NodeMoveActions type, int source, int? destination)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				Node sSource = synologenRepository.Node.GetNodeById (source);
				Node sDestination = null;
				if (destination != null) {
					sDestination = synologenRepository.Node.GetNodeById ((int) destination);
				}
				switch (type) {
					case NodeMoveActions.MoveUp:
						synologenRepository.Node.MoveNode (new Node {Id = source, Parent = sSource.Parent, Order = sSource.Order + 1});
						break;

					case NodeMoveActions.MoveDown:
						synologenRepository.Node.MoveNode (new Node { Id = source, Parent = sSource.Parent, Order = sSource.Order - 1 });
						break;

					case NodeMoveActions.MoveAfter:
						if (sDestination == null) {
							throw new NodeException ("Not valid move operation.", NodeErrors.MoveToForbidden);
						}
						
						synologenRepository.Node.MoveNode (
							new Node { Id = source, Parent = sDestination.Parent, Order = sDestination.Order + 1 });
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
		/// Fetches a specified node.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="fillObjects">Fill all objects.</param>

		public Node GetNode (int nodeId, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Node> (n => n.NodeSupplierConnections);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Files);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Documents);
					synologenRepository.AddDataLoadOptions<Node> (n => n.CreatedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ChangedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.LockedBy);

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

				return synologenRepository.Node.GetNodeById (nodeId);
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
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Node> (n => n.NodeSupplierConnections);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Files);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Documents);
					synologenRepository.AddDataLoadOptions<Node> (n => n.CreatedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ChangedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.LockedBy);

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

				return synologenRepository.Node.GetNodeByName (parent, name);
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
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Node> (n => n.NodeSupplierConnections);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Files);
					synologenRepository.AddDataLoadOptions<Node> (n => n.Documents);
					synologenRepository.AddDataLoadOptions<Node> (n => n.CreatedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ChangedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Node> (n => n.LockedBy);

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

				if ((parent == null) && (name == null)) {
					return synologenRepository.Node.GetRootNodes (onlyActive, onlyApproved);
				}

				if (name == null) {
					return synologenRepository.Node.GetChildNodes ((int) parent, onlyActive, onlyApproved);
				}

				if (parent == null) {
					return synologenRepository.Node.GetNodesByName (name, onlyActive, onlyApproved);
				}

				return synologenRepository.Node.GetNodesByName ((int) parent, name, onlyActive, onlyApproved);
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

		#endregion
	}
}
