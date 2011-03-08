using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.LensSubscription.BGWebService.App.Services;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGWebService.Test.TestHelpers
{
	public abstract class BGWebServiceTestBase : BehaviorActionTestbase<App.Services.BGWebService>
	{
		protected IAutogiroPayerRepository AutogiroPayerRepository;
		protected IBGWebServiceDTOParser BGWebServiceDTOParser;
		protected IBGConsentToSendRepository BGConsentToSendRepository;

		protected override void SetUp()
		{
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();
			BGConsentToSendRepository = A.Fake<IBGConsentToSendRepository>();
			var realParser = new BGWebServiceDTOParser();
			BGWebServiceDTOParser = A.Fake<IBGWebServiceDTOParser>(x => x.Wrapping(realParser));
		}

		protected override App.Services.BGWebService GetTestEntity()
		{
			return new App.Services.BGWebService(
				AutogiroPayerRepository, 
				BGConsentToSendRepository,
				BGWebServiceDTOParser);
		}

		protected Exception TryCatchException(Action action)
		{
			try
			{
				action.Invoke();	
			}
			catch(Exception ex)
			{
				return ex;
			}
			return null;
		}
	}
}