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
		public void GetErrorTextString()
		{
			string errorTextString = BUtilities.GetErrorTextString (PropertyValues.NodeErrorsNameExist);

			Assert.AreEqual("Det valda namnet för noden finns redan. Det måste vara unikt inom samma gren.", errorTextString, "Wrong string fetched.");
		}
	}
}
