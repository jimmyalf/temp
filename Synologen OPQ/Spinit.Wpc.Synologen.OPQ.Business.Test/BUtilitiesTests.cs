using NUnit.Framework;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description ("The unit tests for the utilities business layer.")]
	public class BUtilitiesTests
	{
		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
		}

		[Test, Explicit, Description ("Creates a document."), Category ("Document")]
		public void CreateDocument ()
		{
			string errorTextString = BUtilities.GetErrorTextString ("NodeErrors_NameExist");

			Assert.AreEqual ("The node exist.", errorTextString, "Wrong string fetched.");
		}
	}
}
