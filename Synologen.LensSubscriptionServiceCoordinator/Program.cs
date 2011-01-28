using System;
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
			loggingService.LogInfo("Taskrunner started execution");
			var tasks = ObjectFactory.GetAllInstances<ITask>() ?? Enumerable.Empty<ITask>();
			loggingService.LogInfo("Taskrunner scan found {0} tasks ({1})", 
				tasks.Count(), 
				tasks.Select(x => x.TaskName).Aggregate((taskA,taskB) => taskA + ", " + taskB));
			foreach (var task in tasks)
			{
				ExecuteLoggedTask(task, loggingService);
				
			}
			loggingService.LogInfo("Taskrunner finished execution");
		}

		public static void ExecuteLoggedTask(ITask task, ILoggingService loggingService)
		{
			try
			{
				loggingService.LogInfo("Executing {0} task", task.TaskName);
				task.Execute();
				loggingService.LogInfo("Finished executing {0} task", task.TaskName);
			}
			catch(Exception ex)
			{
				loggingService.LogError(String.Format("Got exception while executing \"{0}\"", task.TaskName), ex);
			}
		}
	}
}