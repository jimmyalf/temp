using NUnit.Framework;
using Shouldly;
using Synologen.LensSubscription.BGWebService.Test.TestHelpers;

namespace Synologen.LensSubscription.BGWebService.Test
{
	[TestFixture, Category("BGWebServiceTests")]
	public class When_testing_BGWebService_connection : BGWebServiceTestBase
	{
		private bool returnValue;

		public When_testing_BGWebService_connection()
		{
			Context = () => { };
			Because = service => 
			{
				returnValue = service.TestConnection();
			};
		}

		[Test]
		public void Returned_value_is_true()
		{
			returnValue.ShouldBe(true);
		}
	}
}