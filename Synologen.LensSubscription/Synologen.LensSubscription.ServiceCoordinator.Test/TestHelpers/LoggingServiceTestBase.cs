using System;
using log4net;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.App.Logging;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
{
	[TestFixture]
	public abstract class LoggingServiceTestBase
	{
		protected string format = "Testar {0} {1} {2} med {3}";
		protected string param1 = "att";
		protected string param2 = "skapa";
		protected string param3 = "meddelande";
		protected string param4 = "string.Format";
		protected Mock<ILog> MockedLogger;
		protected Mock<IEventLoggingService> MockedEventLogger;

		protected LoggingServiceTestBase()
		{
			MockedLogger = new Mock<ILog>();
			MockedEventLogger = new Mock<IEventLoggingService>();
			Context = () => { };
			Because = logger => { throw new AssertionException("An action for Because has not been set!"); };
		}

		[TestFixtureSetUp]
		protected void SetUpTest()
		{
			Context();
			Because(new Log4NetLogger(MockedLogger.Object, MockedEventLogger.Object));
		}

		protected Action Context;
		protected Action<ILoggingService> Because;
	}
}