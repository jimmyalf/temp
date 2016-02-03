using log4net;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Web.BGC.App.Logging
{
	public static class LogFactory
	{
		public static ILog CreateLogger()
		{
			return LogManager.GetLogger("BGWebService");
		}

		//public static IEventLoggingService CreateEventLogger()
		//{
		//    return new EventLogLogger("BGWebService");
		//}

		public static ILoggingService CreateLoggingService()
		{
			return new Log4NetLogger(CreateLogger()/*, CreateEventLogger()*/);
		}
	}
}