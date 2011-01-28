using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Bootstrapper.Bootstrap();
			var loggingService = ObjectFactory.GetInstance<ILoggingService>();
			loggingService.LogInfo("Taskrunner started");
			var tasks = ObjectFactory.GetAllInstances<ITask>() ?? Enumerable.Empty<ITask>();
			loggingService.LogInfo("Taskrunner scan found {0} tasks ({1})", 
				tasks.Count(), 
				tasks.Select(x => x.TaskName).Aggregate((taskA,taskB) => taskA + ", " + taskB));
			

		}
	}
}