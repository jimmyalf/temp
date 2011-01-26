using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator.Tasks
{
	public abstract class TaskBase : ITask
	{
		protected ILoggingService LoggingService { get; private set; }
		private const string DefaultTaskName = "Untitled task";

		protected TaskBase(string taskName,  ILoggingService loggingService)
		{
			TaskName = taskName ?? DefaultTaskName;
			LoggingService = loggingService;
		}

		public virtual void RunLoggedTask(Action action)
		{
			try
			{
				LoggingService.LogInfo("Started {0} Task Execution", TaskName);
				action.Invoke();
				LoggingService.LogInfo("Finished {0} Task Execution", TaskName);
			}
			catch(Exception ex)
			{
				LoggingService.LogError(String.Format("Caught exception while executing \"{0}\"", TaskName),ex);
			}
		}

		public abstract void Execute();

		public virtual string TaskName { get; private set; }
	}
}