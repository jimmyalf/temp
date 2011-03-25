using FakeItEasy;
using log4net;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Synologen.LensSubscription.ServiceCoordinator.App.Logging;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	[TestFixture]
	public abstract class CommonTaskTestBase : BehaviorTestBase<ITask>
	{
		protected Mock<IBGWebServiceClient> MockedWebServiceClient;
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ISubscriptionErrorRepository> MockedSubscriptionErrorRepository;
		protected Mock<ILog> MockedLogger;
		protected Mock<IEventLoggingService> MockedEventLoggingService;
		protected Mock<ITransactionRepository> MockedTransactionRepository;
		protected Log4NetLogger LoggingService;
		protected ITaskRepositoryResolver TaskRepositoryResolver;
		protected IAutogiroPaymentService AutogiroPaymentService;

		protected override void SetUp()
		{
			MockedWebServiceClient = new Mock<IBGWebServiceClient>();
			MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			MockedLogger = new Mock<ILog>();
			MockedEventLoggingService = new Mock<IEventLoggingService>();
			MockedSubscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
			MockedTransactionRepository = new Mock<ITransactionRepository>();
			LoggingService = new Log4NetLogger(MockedLogger.Object, MockedEventLoggingService.Object);
			TaskRepositoryResolver = A.Fake<ITaskRepositoryResolver>();
			AutogiroPaymentService = A.Fake<IAutogiroPaymentService>();
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ISubscriptionRepository>()).Returns(MockedSubscriptionRepository.Object);
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ISubscriptionErrorRepository>()).Returns(MockedSubscriptionErrorRepository.Object);
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ITransactionRepository>()).Returns(MockedTransactionRepository.Object);
		}

		protected abstract ITask GetTask();
		protected override ITask GetTestModel() { return GetTask(); }

		protected ExecutingTaskContext ExecutingTaskContext
		{
			get
			{
				return new ExecutingTaskContext(TestModel, TaskRepositoryResolver);
			}
		}
	}
}