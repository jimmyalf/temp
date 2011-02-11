using log4net;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.App.Logging;

namespace Spinit.Wpc.Synologen.LensSubscription.BGServiceCoordinator.Logging
{
	public static class LogFactory
	{
		public static ILog CreateLogger()
		{
			return LogManager.GetLogger("BGTaskRunner");
		}

		public static IEventLoggingService CreateEventLogger()
		{
			return new EventLogLogger("BGTaskRunner");
		}

		public static ILoggingService CreateLoggingService()
		{
			return new Log4NetLogger(CreateLogger(), CreateEventLogger());
		}
	}
}