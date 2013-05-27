using System;
using Spinit.Wpc.Synologen.Core.Domain.Testing;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public abstract class TaskBase : ITask
	{
		private readonly ILoggingService _loggingService;
		private const string DefaultTaskName = "Untitled task";

		protected TaskBase(string taskName,  ILoggingService loggingService)
		{
			_loggingService = loggingService;
			TaskName = taskName ?? DefaultTaskName;
		}

		protected TaskBase(string taskName,  ILoggingService loggingService, Enum taskOrder) : this(taskName, loggingService)
		{
			TaskOrder = taskOrder.ToInteger();
		}

		public virtual void RunLoggedTask(Action action)
		{
			try
			{
				LogInfo("Started Task Execution", TaskName);
				action.Invoke();
				LogInfo("Finished Task Execution", TaskName);
			}
			catch(Exception ex)
			{
				LogError(String.Format("{0}: Caught exception while executing task", TaskName),ex);
				if(TestRunnerDetector.IsRunningFromNunit) throw;
			}
		}

		public virtual void LogDebug(string format, params object[] parameters)
		{
			_loggingService.LogDebug(LogPrefix + format, parameters);
		}

		public virtual void LogError(string message, Exception ex)
		{
			_loggingService.LogError(LogPrefix + message, ex);
		}


		public virtual void LogInfo(string format, params object[] parameters)
		{
			_loggingService.LogInfo(LogPrefix + format, parameters);
		}

		protected virtual string LogPrefix { get { return String.Format("{0}: ", TaskName); } }

		public abstract void Execute(ExecutingTaskContext context);

		public virtual string TaskName { get; private set; }

		public int TaskOrder { get; protected set; }
	}
}