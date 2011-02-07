using log4net;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.App.Logging
{
	public static class LogFactory
	{
		public static ILog CreateLogger()
		{
			return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public static IEventLoggingService CreateEventLogger()
		{
			return new EventLogLogger("TaskRunner");
		}

		public static ILoggingService CreateLoggingService()
		{
			return new Log4NetLogger(CreateLogger(), CreateEventLogger());
		}
	}
}