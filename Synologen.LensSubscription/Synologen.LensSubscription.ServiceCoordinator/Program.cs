using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.App.Logging;
using Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator.App.Services;

namespace Spinit.Wpc.Synologen.LensSubscription.ServiceCoordinator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var loggingService = LogFactory.CreateLoggingService();
			loggingService.LogDebug("Taskrunner bootstrapping starting...");
			Bootstrapper.Bootstrap();
			loggingService.LogDebug("Taskrunner bootstrapping finished.");
			var taskrunner = new TaskRunnerService(loggingService);
			taskrunner.Run();
		}
	}
}