using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.App.Logging;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var loggingService = LogFactory.CreateLoggingService();
			try
			{
				loggingService.LogInfo("Taskrunner started execution");
				RunTasks(loggingService);
				loggingService.LogInfo("Taskrunner finished execution");
			}
			catch(Exception ex)
			{
				loggingService.LogError("Taskrunner caught an exception", ex);
			}
		}

		public static void RunTasks(ILoggingService loggingService)
		{
			loggingService.LogDebug("Taskrunner bootstrapping starting...");
			Bootstrapper.Bootstrap();
			loggingService.LogDebug("Taskrunner bootstrapping finished.");
			loggingService.LogDebug("Taskrunner fetching tasks from container...");
			var tasks = ObjectFactory.GetAllInstances<ITask>() ?? Enumerable.Empty<ITask>();
			loggingService.LogInfo("Taskrunner scan found {0} tasks ({1})", tasks.Count(), tasks.Select(x => x.TaskName).Aggregate((taskA,taskB) => taskA + ", " + taskB));
			tasks.Each(task => ExecuteLoggedTask(task, loggingService));
		}


		public static void ExecuteLoggedTask(ITask task, ILoggingService loggingService)
		{
			try
			{
				loggingService.LogInfo("Taskrunner: Executing {0} task", task.TaskName);
				task.Execute();
				loggingService.LogInfo("Taskrunner: Finished executing {0} task", task.TaskName);
			}
			catch(Exception ex)
			{
				loggingService.LogError(String.Format("Taskrunner got exception while executing \"{0}\"", task.TaskName), ex);
			}
		}
	}
}