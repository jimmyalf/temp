using Spinit.Wpc.Synologen.Core.Domain.Services;
using log4net;

namespace Synologen.Service.Client.OrderEmailSender.Logging
{
	public static class LogFactory
	{
		public static ILog CreateLogger()
		{
			return LogManager.GetLogger("OrderEmailSender");
		}

		public static ILoggingService CreateLoggingService()
		{
			return new Log4NetLogger(CreateLogger());
		}
	}
}