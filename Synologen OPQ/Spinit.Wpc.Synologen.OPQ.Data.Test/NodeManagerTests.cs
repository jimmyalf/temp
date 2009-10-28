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
				1,
				"Admin");
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
			_configuration = null;
			_context = null;
		}

		[Test, Description ("Creates, fetches, updates and deletes a node."), Category ("CruiseControl")]
		public void NodeTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new node
				const string name = "TestNode";
				synologenRepository.Node.Insert (new Node {Name = name, IsActive = true});

				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetInsertedNode ();

				Assert.IsNotNull (node, "Node is null.");

				// Fetch the node
				Node fetchNode = synologenRepository.Node.GetNodeById (node.Id);

				Assert.IsNotNull (fetchNode, "Fetched node is null.");
				Assert.AreEqual (name, fetchNode.Name, "Name not correct!");

				// Update node
				const string nameUpdate = "TestNode Updated";
				fetchNode.Name = nameUpdate;
				synologenRepository.Node.Update (fetchNode);

				synologenRepository.SubmitChanges ();

				// ReFetch the node
				fetchNode = synologenRepository.Node.GetNodeById (node.Id);

				Assert.IsNotNull (fetchNode, "Fetched node is null.");
				Assert.AreEqual (nameUpdate, fetchNode.Name, "Update name not correct!");

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

		[Test, Description ("Searches for nodes."), Category ("CruiseControl")]
		public void NodeSearchTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.Node.GetNumberOfChilds (null);

				Assert.AreEqual (2, count, "Wrong numer of roots.");

				count = synologenRepository.Node.GetNumberOfChilds (1);
				
				Assert.AreEqual (2, count, "Wrong numer of childs.");

				List<Node> nodes = (List<Node>) synologenRepository.Node.GetRootNodes (true);

				Assert.IsNotEmpty (nodes, "Nodes (root) is empty (only-active).");
				Assert.AreEqual (1, nodes.Count, "Wrong (root) number of nodes (only-active).");
				
				nodes = (List<Node>) synologenRepository.Node.GetRootNodes (false);

				Assert.IsNotEmpty (nodes, "Nodes (root) is empty.");
				Assert.AreEqual (2, nodes.Count, "Wrong (root) number of nodes.");
				
				nodes = (List<Node>) synologenRepository.Node.GetChildNodes (1, true);

				Assert.IsNotEmpty (nodes, "Nodes is empty (only-active).");
				Assert.AreEqual (1, nodes.Count, "Wrong number of nodes (only-active).");

				nodes = (List<Node>) synologenRepository.Node.GetChildNodes (1, false);

				Assert.IsNotEmpty (nodes, "Nodes is empty.");
				Assert.AreEqual (2, nodes.Count, "Wrong number of nodes.");

				nodes = (List<Node>) synologenRepository.Node.GetListUp (7, true);

				Assert.IsNotEmpty (nodes, "Nodes is empty list up.");
				Assert.AreEqual (4, nodes.Count, "Wrong number of nodes (only-active).");
				Assert.AreEqual ("Test-Root1", nodes [0].Name, "Wrong head.");

				nodes = (List<Node>) synologenRepository.Node.GetListUp (7, false);

				Assert.IsNotEmpty (nodes, "Nodes is empty.");
				Assert.AreEqual (4, nodes.Count, "Wrong number of nodes.");
				Assert.AreEqual ("Test-Root1-Child1-Child1-Child1", nodes [0].Name, "Wrong head.");
			}
		}

		[Test, Description ("Searches for nodes."), Category ("CruiseControl")]
		public void NodeMoveUpDownTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.MoveNode (new Node {Id = 1, Parent = null, Order = 2});
				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetNodeById (1);
				Assert.AreEqual (2, node.Order, "Move down failed (1).");

				node = synologenRepository.Node.GetNodeById (2);
				Assert.AreEqual (1, node.Order, "Move down failed (2).");
			}
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Node.MoveNode (new Node { Id = 1, Parent = null, Order = 1 });
				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetNodeById (1);
				Assert.AreEqual (1, node.Order, "Move up failed (1).");

				node = synologenRepository.Node.GetNodeById (2);
				Assert.AreEqual (2, node.Order, "Move up failed (2).");
			}
		}
	}
}
