using log4net;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Synologen.LensSubscription.ServiceCoordinator.App.Logging;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	[TestFixture]
	public abstract class CommonTaskTestBase : BehaviorTestBase<ITask>
	{
		protected Mock<IBGWebService> MockedWebServiceClient;
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ISubscriptionErrorRepository> MockedSubscriptionErrorRepository;
		protected Mock<ILog> MockedLogger;
		protected Mock<IEventLoggingService> MockedEventLoggingService;
		protected Mock<ITransactionRepository> MockedTransactionRepository;
		protected Log4NetLogger LoggingService;

		protected override void SetUp()
		{
			MockedWebServiceClient = new Mock<IBGWebService>();
			MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			MockedLogger = new Mock<ILog>();
			MockedEventLoggingService = new Mock<IEventLoggingService>();
			MockedSubscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
			MockedTransactionRepository = new Mock<ITransactionRepository>();
			LoggingService = new Log4NetLogger(MockedLogger.Object, MockedEventLoggingService.Object);
		}

		protected abstract ITask GetTask();
		protected override ITask GetTestModel() { return GetTask(); }
	}
}