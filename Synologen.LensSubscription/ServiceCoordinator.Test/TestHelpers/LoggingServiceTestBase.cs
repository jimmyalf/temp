using Spinit.Test;
using log4net;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.ServiceCoordinator.App.Logging;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
{
	public class LoggingServiceTestBase : BehaviorActionTestbase<ILoggingService>
	{
		protected string format = "Testar {0} {1} {2} med {3}";
		protected string param1 = "att";
		protected string param2 = "skapa";
		protected string param3 = "meddelande";
		protected string param4 = "string.Format";
		protected Mock<ILog> MockedLogger;

		protected override void SetUp()
		{
			MockedLogger = new Mock<ILog>();
		}

		protected override ILoggingService GetTestEntity()
		{
			return new Log4NetLogger(MockedLogger.Object);
		}
	}
}