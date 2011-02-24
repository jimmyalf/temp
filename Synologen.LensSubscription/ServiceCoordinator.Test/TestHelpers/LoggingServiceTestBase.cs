using log4net;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.ServiceCoordinator.App.Logging;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
{
	[TestFixture]
	public class LoggingServiceTestBase : BehaviorTestBase<ILoggingService>
	{
		protected string format = "Testar {0} {1} {2} med {3}";
		protected string param1 = "att";
		protected string param2 = "skapa";
		protected string param3 = "meddelande";
		protected string param4 = "string.Format";
		protected Mock<ILog> MockedLogger;
		protected Mock<IEventLoggingService> MockedEventLogger;

		protected override void SetUp()
		{
			MockedLogger = new Mock<ILog>();
			MockedEventLogger = new Mock<IEventLoggingService>();
		}

		protected override ILoggingService GetTestModel()
		{
			return new Log4NetLogger(MockedLogger.Object, MockedEventLogger.Object);
		}
	}
}