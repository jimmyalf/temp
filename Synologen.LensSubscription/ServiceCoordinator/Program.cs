using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;
using Synologen.LensSubscription.ServiceCoordinator.App.Logging;

namespace Synologen.LensSubscription.ServiceCoordinator
{
	public class Program
	{
		public const string InProductionArgument = "InProduction";

		public static void Main(string[] args)
		{
			var loggingService = LogFactory.CreateLoggingService();
			loggingService.LogDebug("Taskrunner bootstrapping starting...");
			try
			{
				Bootstrapper.Bootstrap();
				loggingService.LogDebug("Taskrunner bootstrapping finished.");
				loggingService.LogDebug("Taskrunner is fetching tasks from container...");		
				var tasks = ObjectFactory.GetAllInstances<ITask>();
				loggingService.LogInfo("Taskrunner scan found {0} tasks ({1})", tasks.Count(), tasks.Select(x => x.TaskName).Aggregate((taskA,taskB) => taskA + ", " + taskB));
				if(IsInProductionEnvironment(args))
				{
					var taskrunner = new TaskRunnerService(loggingService, tasks);
					taskrunner.Run();
				}
				else
				{
					loggingService.LogInfo("Taskrunner was started in debug mode (default) and will therefore not run any tasks.");
					loggingService.LogInfo("To run taskrunner in production mode, supply \"{0}\" as a command line argument", InProductionArgument);
				}
			}
			catch(Exception ex)
			{
				loggingService.LogError("Got exception while executing ServiceCoordinator Task runner", ex);
			}
		}

		private static bool IsInProductionEnvironment(IEnumerable<string> args)
		{
			return args.Where(arg => arg.ToLower().Contains(InProductionArgument.ToLower())).Any();
		}
	}
}