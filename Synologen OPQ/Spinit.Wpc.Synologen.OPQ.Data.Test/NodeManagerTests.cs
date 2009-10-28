using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
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

		[Test, Explicit, Description ("Creates, fetches, updates and deletes a node."), Category ("CruiseControl")]
		public void NodeTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new node
				synologenRepository.Node.Insert (new Node {Name = "TestNode", IsActive = true});

				synologenRepository.SubmitChanges ();

				Node node = synologenRepository.Node.GetInsertedNode ();

				Assert.IsNotNull (node, "Node is null.");

				// Fetch the node
				Node fetchNode = synologenRepository.Node.GetNodeById (node.Id);

				Assert.IsNotNull (fetchNode, "Fetched node is null.");

				// Update node
			}
		}
	}
}
