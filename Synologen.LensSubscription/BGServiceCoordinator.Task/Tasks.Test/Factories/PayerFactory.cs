using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class PayerFactory
	{
		public static AutogiroPayer Get()
		{
			var payer = A.Fake<AutogiroPayer>();
			A.CallTo(() => payer.Id).Returns(54);
			A.CallTo(() => payer.Name).Returns("Adam Bertil Test");
			A.CallTo(() => payer.ServiceType).Returns(AutogiroServiceType.LensSubscription);
			return payer;
		}
	}
}