using log4net;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Logging
{
	public static class Log4NetFactory
	{
		public static ILog Create()
		{
			return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}
	}
}