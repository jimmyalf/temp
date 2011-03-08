using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Synologen.LensSubscription.BGWebService.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGWebService.Test.TestHelpers
{
	public abstract class BGWebServiceTestBase : BehaviorActionTestbase<App.Services.BGWebService>
	{
		protected IAutogiroPayerRepository AutogiroPayerRepository;
		protected BGWebServiceDTOParser BGWebServiceDTOParser;
		protected override void SetUp()
		{
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();
			BGWebServiceDTOParser = new BGWebServiceDTOParser();
		}

		protected override App.Services.BGWebService GetTestEntity()
		{
			return new App.Services.BGWebService(AutogiroPayerRepository, BGWebServiceDTOParser);
		}

	}
}