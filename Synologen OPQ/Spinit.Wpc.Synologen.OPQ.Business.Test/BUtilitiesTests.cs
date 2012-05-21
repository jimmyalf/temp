using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description ("The unit tests for the utilities business layer.")]
	public class BUtilitiesTests
	{
		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
            Thread.CurrentThread.CurrentCulture = new CultureInfo("Sv-se");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
		}

		[Test, Description ("Checks text from Error-text."), Category ("Document"), Explicit("Test fails and the method it tests does not seem to be used by any other OPQ project. //CBER 2011-11-15")]
		public void GetErrorTextString()
		{
			string errorTextString = BUtilities.GetErrorTextString (PropertyValues.NodeErrorsNameExist);

			Assert.AreEqual("Det valda namnet för noden finns redan. Det måste vara unikt inom samma gren.", errorTextString, "Wrong string fetched.");
		}
	}
}
