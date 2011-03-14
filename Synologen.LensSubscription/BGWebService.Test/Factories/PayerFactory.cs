using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGWebService.Test.Factories
{
	public static class PayerFactory 
	{
		public static AutogiroPayer Get()
		{
			var payer = A.Fake<AutogiroPayer>();
			A.CallTo(() => payer.Id).Returns(43);
			A.CallTo(() => payer.Name).Returns("Adam Bertil");
			A.CallTo(() => payer.ServiceType).Returns(AutogiroServiceType.LensSubscription);
			return payer;
		}
	}
}