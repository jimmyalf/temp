using System;
using log4net;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.App.Logging;

namespace Synologen.ServiceCoordinator.Test.TestHelpers
{
	[TestFixture]
	public abstract class TaskTestBase
	{
		protected Mock<IBGWebService> MockedWebServiceClient;
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ILog> MockedLogger;
		protected Mock<IEventLoggingService> MockedEventLoggingService;
		protected Log4NetLogger LoggingService;

		protected TaskTestBase()
		{
			MockedWebServiceClient = new Mock<IBGWebService>();
			MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			MockedLogger = new Mock<ILog>();
			MockedEventLoggingService = new Mock<IEventLoggingService>();
			LoggingService = new Log4NetLogger(MockedLogger.Object, MockedEventLoggingService.Object);
			
			Context = () => { };
			Because = logger => { throw new AssertionException("An action for Because has not been set!"); };
		}

		[TestFixtureSetUp]
		protected void SetUpTest()
		{
		    Context();
		    Because(GetTask());
		}

		protected abstract ITask GetTask();

		protected Action Context;
		protected Action<ITask> Because;
	}
}