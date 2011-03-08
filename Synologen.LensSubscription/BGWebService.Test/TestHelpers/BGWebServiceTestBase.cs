using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGWebService.Test.TestHelpers
{
	public abstract class BGWebServiceTestBase : BehaviorActionTestbase<App.Services.BGWebService>
	{
		private IAutogiroPayerRepository AutogiroPayerRepository;
		protected override void SetUp()
		{
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();
		}

		protected override App.Services.BGWebService GetTestEntity()
		{
			return new App.Services.BGWebService(AutogiroPayerRepository);
		}
	}
}