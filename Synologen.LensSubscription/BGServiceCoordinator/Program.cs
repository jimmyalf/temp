using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;
using Synologen.LensSubscription.BGServiceCoordinator.App;
using Synologen.LensSubscription.BGServiceCoordinator.App.Logging;
using Synologen.LensSubscription.ServiceCoordinator.Core.IoC;
using Synologen.LensSubscription.ServiceCoordinator.Core.TaskRunner;

namespace Synologen.LensSubscription.BGServiceCoordinator
{
	class Program
	{
		public const string InProductionArgument = "InProduction";
		public const string InTestArgument = "Test";

		public static void Main(string[] args)
		{
			var loggingService = LogFactory.CreateLoggingService();
			SetMode(args, loggingService);
			loggingService.LogDebug("Taskrunner bootstrapping starting...");
			try
			{
				Bootstrapper.Bootstrap();
				loggingService.LogDebug("Taskrunner bootstrapping finished.");
				loggingService.LogDebug("Taskrunner is fetching tasks from container...");		
				var tasks = ObjectFactory.GetAllInstances<ITask>();
				var taskRepositoryResolver = new TaskRepositoryResolver();
				loggingService.LogInfo(GetTaskScanLogMessage(tasks));
				if(Mode.Current != RunningMode.Debug)
				{
					var taskrunner = new TaskRunnerService(loggingService, tasks, taskRepositoryResolver);
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
				loggingService.LogError("Got exception while executing BGServiceCoordinator Task runner", ex);
			}
		}

		private static void SetMode(IEnumerable<string> args, ILoggingService loggingService)
		{
			if(CheckRunningEnvironment(args, InProductionArgument))
			{
				Mode.Current = RunningMode.InProduction;
				loggingService.LogInfo("Taskrunner was started in production mode.");
			}
			else if(CheckRunningEnvironment(args, InTestArgument))
			{
				Mode.Current = RunningMode.Test;
				loggingService.LogInfo("Taskrunner was started in test mode.");
			}
			else
			{
				Mode.Current = RunningMode.Debug;
				loggingService.LogInfo("Taskrunner was started in debug mode.");
			}
		}

		private static bool CheckRunningEnvironment(IEnumerable<string> args, string valueToCheckFor)
		{
			return args.Where(arg => arg.ToLower().Contains(valueToCheckFor.ToLower())).Any();
		}

		private static string GetTaskScanLogMessage(IEnumerable<ITask> tasks)
		{
			if(tasks == null || tasks.Count() == 0)
			{
				return "Taskrunner scan found no tasks";
			}
			return String.Format("Taskrunner scan found {0} tasks ({1})", 
				tasks.Count(),
				tasks.Select(x => x.TaskName).Aggregate((taskA, taskB) => taskA + ", " + taskB));
		}
	}
}