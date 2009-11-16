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

		[Test, Description ("Checks text from Error-text."), Category ("Document")]
		public void CreateDocument ()
		{
			string errorTextString = BUtilities.GetErrorTextString (PropertyValues.NodeErrorsNameExist);

			Assert.AreEqual (PropertyValues.NodeErrorsResult, errorTextString, "Wrong string fetched.");
		}
	}
}
