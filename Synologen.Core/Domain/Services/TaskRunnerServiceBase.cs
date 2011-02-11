using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public abstract class TaskRunnerServiceBase : ITaskRunnerService
	{
		private readonly ILoggingService _loggingService;

		protected TaskRunnerServiceBase(ILoggingService loggingService)
		{
			_loggingService = loggingService;
		}

		public virtual void Run()
		{
			try
			{
				_loggingService.LogInfo("Taskrunner started execution");
				RunTasks(_loggingService);
				_loggingService.LogInfo("Taskrunner finished execution");
			}
			catch(Exception ex)
			{
				_loggingService.LogError("Taskrunner caught an exception", ex);
			}	
		}

		protected virtual void RunTasks(ILoggingService loggingService)
		{
			loggingService.LogDebug("Taskrunner fetching tasks from container...");
			var tasks = GetTasks() ?? Enumerable.Empty<ITask>();
			loggingService.LogInfo("Taskrunner scan found {0} tasks ({1})", tasks.Count(), tasks.Select(x => x.TaskName).Aggregate((taskA,taskB) => taskA + ", " + taskB));
			tasks.Each(task => ExecuteLoggedTask(task, loggingService));
		}

		protected virtual void ExecuteLoggedTask(ITask task, ILoggingService loggingService)
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

		protected abstract IEnumerable<ITask> GetTasks();
	}
}