using System;
using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.Service.Web.BGC.App.Services;

namespace Synologen.LensSubscription.BGWebService.Test.TestHelpers
{
	public abstract class BGWebServiceTestBase : BehaviorActionTestbase<Service.Web.BGC.App.Services.BGWebService>
	{
		protected IAutogiroPayerRepository AutogiroPayerRepository;
		protected IBGWebServiceDTOParser BGWebServiceDTOParser;
		protected IBGConsentToSendRepository BGConsentToSendRepository;
		protected IBGPaymentToSendRepository BGPaymentToSendRepository;
		protected IBGReceivedPaymentRepository BGReceivedPaymentRepository;
		protected IBGReceivedErrorRepository BGReceivedErrorRepository;
		protected IBGReceivedConsentRepository BGReceivedConsentRepository;
		protected ILoggingService LoggingService;

		protected override void SetUp()
		{
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();
			BGConsentToSendRepository = A.Fake<IBGConsentToSendRepository>();
			BGPaymentToSendRepository = A.Fake<IBGPaymentToSendRepository>();
			BGReceivedPaymentRepository = A.Fake<IBGReceivedPaymentRepository>();
			BGReceivedErrorRepository = A.Fake<IBGReceivedErrorRepository>();
			BGReceivedConsentRepository = A.Fake<IBGReceivedConsentRepository>();
			LoggingService = A.Fake<ILoggingService>();
			var realParser = new BGWebServiceDTOParser();
			BGWebServiceDTOParser = A.Fake<IBGWebServiceDTOParser>(x => x.Wrapping(realParser));
		}

		protected override Service.Web.BGC.App.Services.BGWebService GetTestEntity()
		{
			return new Service.Web.BGC.App.Services.BGWebService(
				AutogiroPayerRepository, 
				BGConsentToSendRepository,
				BGPaymentToSendRepository,
				BGReceivedPaymentRepository,
				BGReceivedErrorRepository,
				BGReceivedConsentRepository,
				BGWebServiceDTOParser,
				LoggingService);
		}

		protected Exception CatchException(Action action)
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