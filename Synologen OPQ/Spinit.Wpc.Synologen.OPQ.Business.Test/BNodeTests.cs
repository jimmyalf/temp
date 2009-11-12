using System.Threading;
using NUnit.Framework;
using Spinit.Wpc.Synologen.OPQ.Business.Test.Properties;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description("The unit tests for the node business layer.")]
	public class BNodeTests
	{
		private Configuration _configuration;
		private Context _context;
		private TestInit _init;

		[SetUp, Description("Initialize.")]
		public void NodeManagerInit()
		{
			_configuration = new Configuration(
				Settings.Default.ConnectionString,
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context(
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				1,
				"Admin");
			_init = new TestInit(_configuration);
			_init.InitDatabase();
		}

		[TearDown, Description("Close.")]
		public void NodeManagerCleanUp()
		{
			_init.DeleteDatabase();
			_configuration = null;
			_context = null;
		}

		[Test]
		[Description ("Should throw exist exception")]
		[Category ("Node-Exceptions")]
		public void NodeExistsExceptionOnRootLevel ()
		{
			try {
				const string rootName = "node";
				BNode bNode = new BNode (_context);
				bNode.CreateNode (null, rootName);
				bNode.CreateNode (null, rootName);
				Assert.Fail ("Create node should not be possible with same name");
			}
			catch (NodeException ex) {
				Assert.AreEqual ("Name exist.", ex.Message, "Exception message not as expected");
				Assert.AreEqual (NodeErrors.NameExist, (NodeErrors) ex.ErrorCode);
			}
		}

		[Test]
		[Description ("Should throw exist exception")]
		[Category ("Node-Exceptions")]
		public void NodeExistsExceptionOnChildLevel ()
		{
			const string rootName = "node";
			const string child1 = "child";
			const string child2 = "child";
			try {
				BNode bNode = new BNode (_context);
				Node node = bNode.CreateNode (null, rootName);
				bNode.CreateNode (node.Id, child1);
				bNode.CreateNode (node.Id, child2);
				Assert.Fail ("Create node should not be possible with same name");
			}
			catch (NodeException ex) {
				Assert.AreEqual ("Name exist.", ex.Message, "Exception message not as expected");
				Assert.AreEqual (NodeErrors.NameExist, (NodeErrors) ex.ErrorCode);
			}
		}

		[Test]
		[Description ("Should throw exist exception")]
		[Category ("Node-Exceptions")]
		public void NodeDoesNotExistsByNameException ()
		{
			var bNode = new BNode (_context);
			try {
				bNode.GetNode (null, "FakeNode", true);
				Assert.Fail ("Node should not be found");
			}
			catch (ObjectNotFoundException ex) {
				Assert.AreEqual ("Node not found.", ex.Message, "Exception message not as expected");
				Assert.AreEqual (ObjectNotFoundErrors.NodeNotFound, (ObjectNotFoundErrors) ex.ErrorCode);
			}
		}

		[Test]
		[Description ("Should throw exist exception")]
		[Category ("Node-Exceptions")]
		public void NodeDoesNotExistsByNameAndParentException ()
		{
			var bNode = new BNode (_context);
			try {
				bNode.GetNode (1, "FakeNode", true);
				Assert.Fail ("Node should not be found");
			}
			catch (ObjectNotFoundException ex) {
				Assert.AreEqual ("Node not found.", ex.Message, "Exception message not as expected");
				Assert.AreEqual (ObjectNotFoundErrors.NodeNotFound, (ObjectNotFoundErrors) ex.ErrorCode);
			}
		}

		[Test, Description("Creates a root node."), Category("Node")]
		public void CreateRootNode()
		{
			const string rootName = "root";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);
			Assert.AreEqual(rootName,node.Name);
			Assert.AreEqual(1,node.Order);
		}


		[Test, Description("Creates a child node."), Category("Node")]
		public void CreateChildNode()
		{
			const string rootName = "root";
			const string childName = "child";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);
			var child = bNode.CreateNode(node.Id, childName);
			Assert.AreEqual(childName, child.Name);
			Assert.AreEqual(1, node.Order);
		}


		[Test, Description ("Creates a root node and fetches it. Compares results"), Category ("Node")]
		public void CreateAndFetchRootNode ()
		{
			const string rootName = "root";
			BNode bNode = new BNode (_context);
			Node createdNode = bNode.CreateNode (null, rootName);
			Node fetchedNode = bNode.GetNode (createdNode.Id, true);
			Assert.AreEqual (_context.UserName, fetchedNode.CreatedByName, "Username different from expected");
			Assert.AreEqual (_context.UserId, fetchedNode.CreatedById, "User id different from expected");
			Assert.IsNull (fetchedNode.ApprovedByName, "ApprovedByName not null");
			Assert.IsNull (fetchedNode.ApprovedById, "ApprovedById not null");
			Assert.IsNull (fetchedNode.ChangedByName, "ChangedByName not null");
			Assert.IsNull (fetchedNode.ChangedById, "ChangedById not null");
			Assert.IsNull (fetchedNode.Parent, "Parent not null");
			Assert.IsNull (fetchedNode.ParentNode, "ParentNode by not null");
			Assert.AreEqual (rootName, fetchedNode.Name, "Name not equal");
			Assert.AreEqual (true, fetchedNode.IsActive, "Node should be active after create");
		}

		[Test, Description ("Creates a child node and fetches it. Compares results"), Category ("Node")]
		public void CreateAndFetchChildNode ()
		{
			const string rootName = "root";
			const string childName = "child";
			BNode bNode = new BNode (_context);
			Node rootNode = bNode.CreateNode (null, rootName);
			Node childNode = bNode.CreateNode (rootNode.Id, childName);
			Node fetchedNode = bNode.GetNode (childNode.Id, true);
			Assert.AreEqual (_context.UserName, fetchedNode.CreatedByName);
			Assert.AreEqual (_context.UserId, fetchedNode.CreatedById);
			Assert.IsNull (fetchedNode.ApprovedByName, "ApprovedByName not null");
			Assert.IsNull (fetchedNode.ApprovedById, "ApprovedById not null");
			Assert.IsNull (fetchedNode.ChangedByName, "ChangedByName not null");
			Assert.IsNull (fetchedNode.ChangedById, "ChangedById not null");
			Assert.AreEqual (rootNode.Id, fetchedNode.Parent, "Parent Id not as expected");
			Assert.AreEqual (rootNode, fetchedNode.ParentNode, "ParentNode not as expected");
			Assert.AreEqual (childName, fetchedNode.Name, "Name not equal");
			Assert.AreEqual (true, fetchedNode.IsActive, "Node should be active after create");
		}

		[Test, Description ("Creates a node and update current node"), Category ("Node")]
		public void CreateAndUpdatesRootNode ()
		{
			const string nodeName = "root";
			const string newNodeName = "newName";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, nodeName);
			Node newNode = bNode.ChangeNode (node.Id, node.Parent, newNodeName);
			Assert.AreEqual (newNodeName, newNode.Name, "The new name are not as expected after change");
			Assert.AreEqual (_context.UserName, newNode.ChangedByName, "ChangedByName not as expected");
			Assert.AreEqual (_context.UserId, newNode.ChangedById, "ChangedByid not as expected");
			newNode = bNode.GetNode (node.Id, true);
			Assert.AreEqual (newNodeName, newNode.Name, "The new name are not as expected after fetch by Id");
			Assert.AreEqual (_context.UserName, newNode.ChangedByName, "ChangedByName not as expected");
			Assert.AreEqual (_context.UserId, newNode.ChangedById, "ChangedByid not as expected");
			newNode = bNode.GetNode (node.Parent, newNode.Name, true);
			Assert.AreEqual (newNodeName, newNode.Name, "The new name are not as expected after fetch by Name");
			Assert.AreEqual (_context.UserName, newNode.ChangedByName, "ChangedByName not as expected");
			Assert.AreEqual (_context.UserId, newNode.ChangedById, "ChangedByid not as expected");
		}

		[Test, Description ("Creates a child node and update current node"), Category ("Node")]
		public void CreateAndUpdatesChildNode ()
		{
			const string nodeName = "root";
			const string childName = "child";
			const string newNodeName = "newName";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, nodeName);
			Node childNode = bNode.CreateNode (node.Id, childName);
			Node newNode = bNode.ChangeNode (childNode.Id, childNode.Parent, newNodeName);
			Assert.AreEqual (newNodeName, newNode.Name, "The new name are not as expected after change");
			Assert.AreEqual (_context.UserName, newNode.ChangedByName, "ChangedByName not as expected");
			Assert.AreEqual (_context.UserId, newNode.ChangedById, "ChangedByid not as expected");
			Assert.AreEqual (node.Id, newNode.Parent, "Parent not as expected");
			newNode = bNode.GetNode (childNode.Id, true);
			Assert.AreEqual (newNodeName, newNode.Name, "The new name are not as expected after fetch by Id");
			Assert.AreEqual (_context.UserName, newNode.ChangedByName, "ChangedByName not as expected after fetch by Id");
			Assert.AreEqual (_context.UserId, newNode.ChangedById, "ChangedByid not as expected after fetch by Id");
			Assert.AreEqual (node, newNode.ParentNode, "ParentNode not as expected after fetch by Id");
			newNode = bNode.GetNode (childNode.Parent, newNode.Name, true);
			Assert.AreEqual (newNodeName, newNode.Name, "The new name are not as expected after fetch by Name");
			Assert.AreEqual (_context.UserName, newNode.ChangedByName, "ChangedByName not as expected after fetch by Name");
			Assert.AreEqual (_context.UserId, newNode.ChangedById, "ChangedByid not as expected after fetch by Name");
			Assert.AreEqual (node, newNode.ParentNode, "ParentNode not as expected after fetch by Name");
		}

		[Test, Description("Creates and locks a child node. Compares results")]
		public void LockChildNode()
		{
			const string nodeName = "root";
			const string childName = "child";
			var bNode = new BNode(_context);
			var rootNode = bNode.CreateNode(null, nodeName);
			var childNode = bNode.CreateNode(rootNode.Id, childName);
			bNode.Lock(childNode.Id);
			childNode = bNode.GetNode(rootNode.Id, childName, true);
			Assert.AreEqual(_context.UserName, childNode.LockedByName, "LockedByName not as expected");
			Assert.AreEqual(_context.UserId, childNode.LockedById, "LockedById not as expected");
		}

		[Test, Description("Creates and approves a child node. Compares results")]
		public void ApproveChildNode()
		{
			const string nodeName = "root";
			const string childName = "child";
			BNode bNode = new BNode(_context);
			Node rootNode = bNode.CreateNode(null, nodeName);
			Node childNode = bNode.CreateNode(rootNode.Id, childName);
			bNode.Publish(childNode.Id);
			childNode = bNode.GetNode(rootNode.Id, childName, true);
			Assert.AreEqual(_context.UserName,childNode.ApprovedByName, "ApprovedByName not as expected");
			Assert.AreEqual(_context.UserId, childNode.ApprovedById, "ApprovedById not as expected");
		}

	//	[Test, Description("Different move methods")]


	}
}
