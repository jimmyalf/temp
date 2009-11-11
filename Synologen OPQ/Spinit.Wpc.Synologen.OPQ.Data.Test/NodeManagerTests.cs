using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data.Test.Properties;

namespace Spinit.Wpc.Synologen.OPQ.Data.Test
{
	[TestFixture, Description ("The unit tests for the data node-manager.")]
	public class NodeManagerTests
	{
		private Configuration _configuration;
		private Context _context;

		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
			_configuration = new Configuration (
				Settings.Default.ConnectionString, 
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context (
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				PropertyValues.UserId,
				PropertyValues.UserName);
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
			_configuration = null;
			_context = null;
		}

		#region Node

		[Test, Description ("Creates, fetches, updates and deletes a node."), Category ("Internal")]
		public void NodeAddUpdateDeleteTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new node
				synologenRepository.Node.Insert (new Node {Name = PropertyValues.NodeName});

				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetInsertedNode ();

				Assert.IsNotNull (node, "Node is null.");

				// Fetch the node
				Node fetchNode = synologenRepository.Node.GetNodeById (node.Id);

				Assert.IsNotNull (fetchNode, "Fetched node is null.");
				Assert.AreEqual (PropertyValues.NodeName, fetchNode.Name, "Name not correct!");

				// Update node
				fetchNode.Name = PropertyValues.NodeNameUpdated;
				synologenRepository.Node.Update (fetchNode);

				synologenRepository.SubmitChanges ();

				// ReFetch the node
				fetchNode = synologenRepository.Node.GetNodeById (node.Id);

				Assert.IsNotNull (fetchNode, "Fetched node is null.");
				Assert.AreEqual (PropertyValues.NodeNameUpdated, fetchNode.Name, "Update name not correct!");

				// Delete the node
				synologenRepository.Node.Delete (fetchNode);

				synologenRepository.SubmitChanges ();
				
				bool found = true;
				try {
					// ReFetch the document
					fetchNode = synologenRepository.Node.GetNodeById (node.Id);

					Assert.IsNull (fetchNode, "Deleted node is not null.");
				}
				catch (ObjectNotFoundException e) {
					if (ObjectNotFoundErrors.NodeNotFound == (ObjectNotFoundErrors) e.ErrorCode) {
						found = false;
					}
				}

				Assert.AreEqual (false, found, "Object still exist.");
			}
		}

		[Test, Description ("Moves nodes up and down."), Category ("Internal")]
		public void NodeMoveUpDownTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.MoveNode (
					new Node
					{
						Id = PropertyValues.MoveNodeId, 
						Parent = null, 
						Order = PropertyValues.NodeOrder2
					});
				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetNodeById (PropertyValues.MoveNodeId);
				Assert.AreEqual (PropertyValues.NodeOrder2, node.Order, "Move down failed (1).");

				node = synologenRepository.Node.GetNodeById (PropertyValues.MovedNodeId);
				Assert.AreEqual (PropertyValues.NodeOrder1, node.Order, "Move down failed (2).");
			}
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.MoveNode (
					new Node
					{
						Id = PropertyValues.MoveNodeId, 
						Parent = null, 
						Order = PropertyValues.NodeOrder1
					});
				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetNodeById (PropertyValues.MoveNodeId);
				Assert.AreEqual (PropertyValues.NodeOrder1, node.Order, "Move up failed (1).");

				node = synologenRepository.Node.GetNodeById (PropertyValues.MovedNodeId);
				Assert.AreEqual (PropertyValues.NodeOrder2, node.Order, "Move up failed (2).");
			}
		}

		[Test, Description ("Fetches the number of active nodes (root)."), Category ("CruiseControl")]
		public void NodeSearchTestNumberOfRootsActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.Node.GetNumberOfChilds (null, true, true);

				Assert.AreEqual (PropertyValues.ActiveNodesRoot, count, "Wrong numer of roots (only active).");
			}
		}

		[Test, Description ("Fetches the number of all nodes (root)."), Category ("CruiseControl")]
		public void NodeSearchTestNumberOfRootsAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.Node.GetNumberOfChilds (null, false, false);

				Assert.AreEqual (PropertyValues.AllNodesRoot, count, "Wrong numer of roots (all).");
			}
		}

		[Test, Description ("Fetches the number of active nodes (child)."), Category ("CruiseControl")]
		public void NodeSearchTestNumberOfChildsActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.Node.GetNumberOfChilds (PropertyValues.ParentNodeId, true, true);

				Assert.AreEqual (PropertyValues.ActiveNodesChild, count, "Wrong numer of childs (only active).");
			}
		}

		[Test, Description ("Fetches the number of all nodes (child)."), Category ("CruiseControl")]
		public void NodeSearchTestNumberOfChildsAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.Node.GetNumberOfChilds (PropertyValues.ParentNodeId, false, false);

				Assert.AreEqual (PropertyValues.AllNodesChild, count, "Wrong numer of childs (all).");
			}
		}

		[Test, Description ("Fetches all active roots."), Category ("CruiseControl")]
		public void NodeSearchTestActiveRoots ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetRootNodes (true, true);

				Assert.IsNotEmpty (nodes, "Nodes (root) is empty (only-active).");
				Assert.AreEqual (PropertyValues.ActiveNodesRoot, nodes.Count, "Wrong (root) number of nodes (only-active).");
			}
		}

		[Test, Description ("Fetches all roots.."), Category ("CruiseControl")]
		public void NodeSearchTestAllRoots ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetRootNodes (false, false);

				Assert.IsNotEmpty (nodes, "Nodes (root) is empty.");
				Assert.AreEqual (PropertyValues.AllNodesRoot, nodes.Count, "Wrong (root) number of nodes.");
			}
		}

		[Test, Description ("Fetches all active childs for a specified node."), Category ("CruiseControl")]
		public void NodeSearchTestActiveChilds ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetChildNodes (PropertyValues.ParentNodeId, true, true);

				Assert.IsNotEmpty (nodes, "Nodes is empty (only-active).");
				Assert.AreEqual (PropertyValues.ActiveNodesChild, nodes.Count, "Wrong number of nodes (only-active).");
			}
		}

		[Test, Description ("Fetches all childs for a specified node."), Category ("CruiseControl")]
		public void NodeSearchTestAllChilds ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetChildNodes (PropertyValues.ParentNodeId, false, false);

				Assert.IsNotEmpty (nodes, "Nodes is empty.");
				Assert.AreEqual (PropertyValues.AllNodesChild, nodes.Count, "Wrong number of nodes.");
			}
		}

		[Test, Description ("Fetches a list of nodes from child to root, root first."), Category ("CruiseControl")]
		public void NodeSearchTestListDown ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetListUp (PropertyValues.ListUpId, true);

				Assert.IsNotEmpty (nodes, "Nodes is empty list up.");
				Assert.AreEqual (PropertyValues.ListCount, nodes.Count, "Wrong number of nodes (only-active).");
				Assert.AreEqual (PropertyValues.ListRootFirstConent, nodes [0].Name, "Wrong head.");
			}
		}

		[Test, Description ("Fetches a list of nodes from child to root, child first."), Category ("CruiseControl")]
		public void NodeSearchTestListUp ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetListUp (PropertyValues.ListUpId, false);

				Assert.IsNotEmpty (nodes, "Nodes is empty.");
				Assert.AreEqual (PropertyValues.ListCount, nodes.Count, "Wrong number of nodes.");
				Assert.AreEqual (PropertyValues.ListChildFirstConenent, nodes [0].Name, "Wrong head.");
			}
		}

		#endregion

		#region Node Supplier

		[Test, Description ("Creates, fetches and deletes a node-supplier-connection."), Category ("Internal")]
		public void NodeSupplierConnectionAddDeleteTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new node
				synologenRepository.Node.Insert (
					new NodeSupplierConnection
					{
						NdeId = PropertyValues.NodeSupplierNodeId, 
						SupId = PropertyValues.UserId
					});

				synologenRepository.SubmitChanges ();

				NodeSupplierConnection nodeSupplierConnection = synologenRepository.Node.GetInsertedNodeSupplierConnection ();

				Assert.IsNotNull (nodeSupplierConnection, "Node-supplier-connection is null.");

				// Fetch the node
				NodeSupplierConnection fetchNodeSupplierConnection = synologenRepository.Node.GetNodeSupplierConnectionById (
					nodeSupplierConnection.NdeId,
					nodeSupplierConnection.SupId);

				Assert.IsNotNull (fetchNodeSupplierConnection, "Fetched node-supplier-connection is null.");

				// Delete the node
				synologenRepository.Node.Delete (fetchNodeSupplierConnection);

				synologenRepository.SubmitChanges ();

				bool found = true;
				try {
					// ReFetch the document
					fetchNodeSupplierConnection = synologenRepository.Node.GetNodeSupplierConnectionById (
						nodeSupplierConnection.NdeId,
						nodeSupplierConnection.SupId);

					Assert.IsNull (fetchNodeSupplierConnection, "Deleted node-supplier-connection is not null.");
				}
				catch (ObjectNotFoundException e) {
					if (ObjectNotFoundErrors.NodeSupplierNotFound == (ObjectNotFoundErrors) e.ErrorCode) {
						found = false;
					}
				}

				Assert.AreEqual (false, found, "Object still exist.");
			}
		}

		[Test, Description ("Fetches a list of node-supplier-connections for a node."), Category ("CruiseControl")]
		public void NodeSupplerConnectionSearchTestNode ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<NodeSupplierConnection> nodeSupplierConnections =
					(List<NodeSupplierConnection>) synologenRepository.Node.GetNodeSupplierConnectionsForNode (
														PropertyValues.NodeSupplierSearchNodeId);

				Assert.IsNotEmpty (nodeSupplierConnections, "Nodes is empty.");
				Assert.AreEqual (PropertyValues.NodeSupplierCount, nodeSupplierConnections.Count, "Wrong number of node-supplier-connections.");
				Assert.AreEqual (PropertyValues.UserId, nodeSupplierConnections [0].SupId, "Wrong supplier.");
			}
		}

		[Test, Description ("Fetches a list of node-supplier-connections for a node."), Category ("CruiseControl")]
		public void NodeSupplerConnectionSearchTestSupplier ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<NodeSupplierConnection> nodeSupplierConnections =
					(List<NodeSupplierConnection>) synologenRepository.Node.GetNodeSupplierConnectionsForSupplier (
														PropertyValues.UserId);

				Assert.IsNotEmpty (nodeSupplierConnections, "Nodes is empty.");
				Assert.AreEqual (PropertyValues.NodeSupplierCount, nodeSupplierConnections.Count, "Wrong number of node-supplier-connections.");
				Assert.AreEqual (PropertyValues.NodeSupplierSearchNodeId, nodeSupplierConnections [0].NdeId, "Wrong supplier.");
			}
		}

		[Test, Description ("Fetches a specific node-supplier-connection."), Category ("CruiseControl")]
		public void NodeSupplerConnectionSearchTestNodeSupplier ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				NodeSupplierConnection nodeSupplierConnection = 
					synologenRepository.Node.GetNodeSupplierConnectionById (
						PropertyValues.NodeSupplierSearchNodeId,
						PropertyValues.UserId);

				Assert.IsNotNull (nodeSupplierConnection, "Node is null.");
			}
		}

		#endregion
	}
}
